using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ZipCodes.Pages.ZipCodeInfoPage
{
    public partial class ZipCodeInfoPage : WebPage
    {
        private List<ZipCodeInformation> zipCodeInfo = new();
        private Dictionary<string, string> googleMapsLinks = new();

        public ZipCodeInfoPage(IWebDriver _driver) : base(_driver)
        {
        }

        public void GetInformationForNumberOfCities(int numberOfCities)
        {
            for (int i = 0; i < numberOfCities; i++)
            {
                GetCollectionOfZipCodeInformationLinksFromResultTable().ElementAt(i).Click();

                zipCodeInfo.Add(new ZipCodeInformation()
                {
                    CityName = CityNameFromSearchResult.Text,
                    StateName = StateNameFromSearchResult.Text,
                    ZipCode = ZipCodeFromSearchResult.Text,
                    Latitude = LatitudeFromSearchResult.Text,
                    Longitude = LongitudeFromSearchResult.Text
                });

                googleMapsLinks.Add($"{CityNameFromSearchResult.Text}-{StateNameFromSearchResult.Text}-{ZipCodeFromSearchResult.Text}.jpg", GetGeneratedGoogleMapsLinkByLatitudeAndLongitude(LatitudeFromSearchResult.Text, LongitudeFromSearchResult.Text));
                Driver.Navigate().Back();
            }
        }

        public string GetGeneratedGoogleMapsLinkByLatitudeAndLongitude(string latitude, string longitude)
        {
            return $"https://maps.google.com/?q={latitude},{longitude}";
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
