using OpenQA.Selenium;

namespace ZipCodes.Pages.GoogleMapsPage
{
    public class GoogleMapsPage : WebPage
    {
        public GoogleMapsPage(IWebDriver _driver) 
            : base(_driver)
        {
        }

        public void TakeScreenshotOfGoogleMapsLinksAndSaveAsFile()
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
