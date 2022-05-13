using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using OpenQA.Selenium;
using ZipCodes.Performance;

namespace ZipCodes
{
    internal class PerformanceTimingService
    {
        public PerformanceTimingService(IWebDriver driver)
        {
            Driver = driver;
            JsDriver = ((IJavaScriptExecutor)driver);
            TestCasePerformanceData = new TestCasePerformanceData();
        }

        public IWebDriver Driver { get; set; }

        public IJavaScriptExecutor JsDriver { get; set; }

        public TestCasePerformanceData TestCasePerformanceData { get; set; }

        public void AddPagePerformanceData(string pageLoadFormula = "performance.timing.loadEventEnd - performance.timing.connectStart")
        {
            try
            {
                string readyMeasure = JsDriver.ExecuteScript($"return {pageLoadFormula}").ToString();
                string jsHeapSize = JsDriver.ExecuteScript($"return performance.memory.usedJSHeapSize").ToString();
                string performanceTiming = JsDriver.ExecuteScript("return JSON.stringify(performance.getEntriesByType('navigation')[performance.getEntriesByType('navigation').length - 1])").ToString();
                string pageUrl = Driver.Url;
                if (TestCasePerformanceData.PagePerformanceData.LastOrDefault()?.PageUrl != pageUrl)
                {
                    TestCasePerformanceData.PagePerformanceData.Add(
                    new PagePerformanceData()
                    {
                        PageUrl = pageUrl,
                        PageTitle = RemoveQuotes(Driver.Title),
                        PagePerformanceTiming = JsonConvert.DeserializeObject<PagePerformanceTiming>(performanceTiming),
                        ReadyMeasure = double.Parse(readyMeasure),
                        JSHeapMemoryUsed = double.Parse(jsHeapSize),
                    });

                    Console.WriteLine($"Page Load time: {readyMeasure} ms.\r\n URL: {pageUrl}");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to fetch performance data from the page due to error: {e.Message}.");
            }
        }

        public void GenerateReport()
        {
            string template = $@"<!DOCTYPE html>
<html>
  <head>
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/chartist.js/latest/chartist.min.css"">
    <script src=""https://cdn.jsdelivr.net/chartist.js/latest/chartist.min.js""></script>
    <script src=""https://codeyellowbv.github.io/chartist-plugin-legend/chartist-plugin-legend.js""></script>
    <style>
        table, th, td {{
          border: 1px solid;
        }}
        table {{
          border-collapse: collapse;
        }}
       .ct-chart {{
           position: relative;
       }}
       .ct-legend {{
           position: relative;
           z-index: 10;
           list-style: none;
           text-align: center;
       }}
       .ct-legend li {{
           position: relative;
           padding-left: 23px;
           margin-right: 10px;
           margin-bottom: 3px;
           cursor: pointer;
           display: inline-block;
       }}
       .ct-legend li:before {{
           width: 12px;
           height: 12px;
           position: absolute;
           left: 0;
           content: '';
           border: 3px solid transparent;
           border-radius: 2px;
       }}
       .ct-legend li.inactive:before {{
           background: transparent;
       }}
       .ct-legend.ct-legend-inside {{
           position: absolute;
           top: 0;
           right: 0;
       }}
       .ct-legend.ct-legend-inside li{{
           display: block;
           margin: 0;
       }}
       .ct-legend .ct-series-0:before {{
           background-color: #d70206;
           border-color: #d70206;
       }}
       .ct-legend .ct-series-1:before {{
           background-color: #f05b4f;
           border-color: #f05b4f;
       }}
       .ct-legend .ct-series-2:before {{
           background-color: #f4c63d;
           border-color: #f4c63d;
       }}
       .ct-legend .ct-series-3:before {{
           background-color: #d17905;
           border-color: #d17905;
       }}
       .ct-legend .ct-series-4:before {{
           background-color: #453d3f;
           border-color: #453d3f;
       }}
    </style>
   </head>
   <div class=""test-data-wrapper"">
    <h1>{TestCasePerformanceData.TestName}</h1>
   <ul>
        <li>Test Execution Time: {TestCasePerformanceData.TestTotalTime} s.</li>
        <li>Test Arrange Time: {TestCasePerformanceData.TestArrangeTime} s.</li>
   </ul>
   </div>

   <div class=""test-data-table"">
    <table><thead>
    <tr><th>Page Name</th><th>Page Url</th><th>Page Load Time (ms.)</th></tr>
    </thead><tbody>
    {string.Join("\r\n", TestCasePerformanceData.PagePerformanceData.Select(page => $"<tr><td>{page.PageTitle}</td><td><a href={page.PageUrl}>{page.PageUrl}</a></td><td>{page.ReadyMeasure}</td></tr>"))}
    </tbody></table></div>
    <h2>Total Page Load Time</h2>
    <div class=""ct-chart ct-perfect-fourth total-time"" style=""height: 90vh; width: 90vw; margin: auto 0""></div>
    <h2>Page Load Time Details</h2>
    <div class=""ct-chart ct-perfect-fourth detailed-time"" style=""height: 90vh; width: 90vw; margin: auto 0""></div>
    <h2>Transfer Size</h2>
    <div class=""ct-chart ct-perfect-fourth transfer-size"" style=""height: 90vh; width: 90vw; margin: auto 0""></div>
    <h2>JS Heap Size</h2>
    <div class=""ct-chart ct-perfect-fourth js-heap-size"" style=""height: 90vh; width: 90vw; margin: auto 0""></div>
<script>
        var totalTimeData = {{
            // A labels array that can contain any sort of values
            labels:
            ['{string.Join("', '", TestCasePerformanceData.PagePerformanceData.Select(p => p.PageTitle).ToArray())}'],
            series: [
                {{""name"": ""ReadyMeasure"", 
                  ""data"": [{string.Join(", ", TestCasePerformanceData.PagePerformanceData.Select(p => p.ReadyMeasure).ToArray())}]}},
            ]
            }};
        var detailTimeData = {{
            // A labels array that can contain any sort of values
            labels:
            ['{string.Join("', '", TestCasePerformanceData.PagePerformanceData.Select(p => p.PageTitle).ToArray())}'],
            series: [
                {{ ""name"": ""PageLoadTime"", ""data"": [{string.Join(", ", TestCasePerformanceData.PagePerformanceData.Select(p => p.PageLoadTime).ToArray())}] }},
                {{ ""name"": ""DOMContentLoadedTime"", ""data"": [{string.Join(", ", TestCasePerformanceData.PagePerformanceData.Select(p => p.DOMContentLoadedTime).ToArray())}] }},
                {{ ""name"": ""DOMInteractive"", ""data"": [{string.Join(", ", TestCasePerformanceData.PagePerformanceData.Select(p => p.DOMInteractive).ToArray())}] }},
                {{ ""name"": ""SSLNegotiationTime"", ""data"": [{string.Join(", ", TestCasePerformanceData.PagePerformanceData.Select(p => p.SSLNegotiationTime).ToArray())}] }},
                {{ ""name"": ""DOMComplete"", ""data"": [{string.Join(", ", TestCasePerformanceData.PagePerformanceData.Select(p => p.DOMComplete).ToArray())}] }},
                ]
                }};

        var transferSizeData = {{
            // A labels array that can contain any sort of values
            labels: 
                ['{string.Join("', '", TestCasePerformanceData.PagePerformanceData.Select(p => p.PageTitle).ToArray())}'],
            series: 
                [{{ ""name"": ""TransferSize"", ""data"": [{string.Join(", ", TestCasePerformanceData.PagePerformanceData.Select(p => p.PagePerformanceTiming.TransferSize).ToArray())}] }},]
            }};
        var jsHeapSizeData = {{
            // A labels array that can contain any sort of values
            labels:
            ['{string.Join("', '", TestCasePerformanceData.PagePerformanceData.Select(p => p.PageTitle).ToArray())}'],
            series: [
                {{ ""name"": ""JSHeapMemoryUsed"", 
                   ""data"": [{string.Join(", ", TestCasePerformanceData.PagePerformanceData.Select(p => p.JSHeapMemoryUsed).ToArray())}] }},]
            }};
        var timeOptions = {{
          // Don't draw the line chart points
          showPoint: true,
          // Disable line smoothing
          lineSmooth: true,
          // X-Axis specific configuration
          axisX: {{
            // We can disable the grid for this axis
            showGrid: true,
            // and also don't show the label
            showLabel: true
          }},
          // Y-Axis specific configuration
          axisY: {{
            // Lets offset the chart a bit from the labels
            offset: 60,
            // The label interpolation function enables you to modify the values
            // used for the labels on each axis. Here we are converting the
            // values into million pound.
            labelInterpolationFnc: function(value) {{
              return value + ' ms.';
            }}
          }},
          plugins: [
              Chartist.plugins.legend({{
                  position: 'bottom'
              }})
          ]
        }};
        var sizeOptions = {{
                    // Don't draw the line chart points
                    showPoint: true,
                    // Disable line smoothing
                    lineSmooth: true,
                    // X-Axis specific configuration
                    axisX: {{
                    // We can disable the grid for this axis
                    showGrid: true,
                    // and also don't show the label
                    showLabel: true
                    }},
                    // Y-Axis specific configuration
                    axisY: {{
                    // Lets offset the chart a bit from the labels
                    offset: 60,
                    // The label interpolation function enables you to modify the values
                    // used for the labels on each axis. Here we are converting the
                    // values into million pound.
                    labelInterpolationFnc: function(value) {{
                        return value + ' bytes';
                    }}
                    }},
                    plugins: [
                        Chartist.plugins.legend({{
                            position: 'bottom'
                        }})
                    ]
                }};

    // Create a new line chart object where as first parameter we pass in a selector
    // that is resolving to our chart container element. The Second parameter
    // is the actual data object.
    new Chartist.Line('.ct-chart.total-time', totalTimeData, timeOptions);
    new Chartist.Line('.ct-chart.detailed-time', detailTimeData, timeOptions);
    new Chartist.Line('.ct-chart.transfer-size', transferSizeData, sizeOptions);
    new Chartist.Line('.ct-chart.js-heap-size', jsHeapSizeData, sizeOptions);
</script>
</html>";

            try
            {
                string filePath = $".\\PerformanceReport-{TestCasePerformanceData.TestName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}.html";
                using (FileStream fileStream = File.Create(filePath))
                {
                    AddText(fileStream, template);
                }

                System.Console.WriteLine($"Link to Performance Report: \r\nfile://{Path.Combine(Environment.CurrentDirectory, filePath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to generate Performance Report due to error: " + ex.Message);
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private static string RemoveQuotes(string input)
        {
            return input.Replace("\"", "").Replace("'", "");
        }
    }
}
