using OpenQA.Selenium;
using System.Collections.Generic;

namespace ZipCodes.Pages.GoogleMapsPage
{
    public class GoogleMapsPage : WebPage
    {
        public GoogleMapsPage(IWebDriver _driver) 
            : base(_driver)
        {
        }

        public void TakeScreenshotOfGoogleMapsLinksAndSaveAsFile(Dictionary<string, string> googleMapsLinks)
        {
            foreach (var googleLinks in googleMapsLinks)
            {
                Driver.Navigate().GoToUrl(googleLinks.Value);

                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(googleLinks.Key, ScreenshotImageFormat.Jpeg);
            }
            googleMapsLinks.Clear();
        }
    }
}
