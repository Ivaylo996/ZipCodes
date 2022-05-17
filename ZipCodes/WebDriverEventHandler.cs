using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;

namespace ZipCodes
{
    internal static class WebDriverEventHandler
    {
        private static PerformanceTimingService performanceTimingService;
        public static PerformanceTimingService PerformanceTimingService
        {
            get { return performanceTimingService; }
            set 
            {
                if (performanceTimingService == null)
                {
                    performanceTimingService = value; 
                }
            }
        }

        public static void FiringDriver_FindingElement(object sender, FindElementEventArgs e)
        {
            Console.WriteLine("Finding Element");

            var WebDriverWait = new WebDriverWait(e.Driver, TimeSpan.FromSeconds(30));
            var js = (IJavaScriptExecutor)e.Driver;
            WebDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public static void FiringDriver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            Console.WriteLine("Exception thrown" + e.ThrownException.ToString());
        }

        public static void FiringDriver_ScriptExecuting(object sender, WebDriverScriptEventArgs e)
        {
            Console.WriteLine("JavaScript Executing");
        }

        public static void FiringDriver_ScriptExecuted(object sender, WebDriverScriptEventArgs e)
        {
            Console.WriteLine("JavaScript Executed");
        }

        public static void FiringDriver_Navigating(object sender, WebDriverNavigationEventArgs e)
        {
            Console.WriteLine("NAVIGATING  - " + e.Url);
        }

        public static void FiringDriver_Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            PerformanceTimingService = new PerformanceTimingService(e.Driver);
            Console.WriteLine("PAGE LOADED - " + e.Url);
            Console.WriteLine("Page Load Time:" + ((IJavaScriptExecutor)e.Driver).ExecuteScript("return performance.getEntriesByType('navigation')[performance.getEntriesByType('navigation').length - 1].duration") + " ms.");
            PerformanceTimingService.AddPagePerformanceData();
        }

        public static void FiringDriver_Clicking(object sender, WebElementEventArgs e)
        {
            Console.WriteLine("Clicking on element with text:" + e.Element.Text);
        }

        public static void FiringDriver_Clicked(object sender, WebElementEventArgs e)
        {
            PerformanceTimingService = new PerformanceTimingService(e.Driver);
            Console.WriteLine("Page Load Time:" + ((IJavaScriptExecutor)e.Driver).ExecuteScript("return performance.getEntriesByType('navigation')[performance.getEntriesByType('navigation').length - 1].duration") + " ms.");
            Console.WriteLine("Page URL:" + e.Driver.Url);
            PerformanceTimingService.AddPagePerformanceData();
        }
    }
}
