using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace ZipCodes.Pages.ZipCodeInfoPage
{
    public partial class ZipCodeInfoPage : WebPage
    {
        private List<ZipCodeInformation> zipCodeInfo = new();
        private Dictionary<string, string> googleMapsLinks = new();

        public ZipCodeInfoPage(IWebDriver _driver) : base(_driver)
        {
        }

        protected override string Url => "";

        public void GetCityInfo(int numberOfCities)
        {
            for (int i = 0; i < numberOfCities; i++)
            {
                CollectAllLinks().ElementAt(i).Click();

                zipCodeInfo.Add(new ZipCodeInformation()
                {
                    CityName = CityNameFromSearchResult.Text,
                    StateName = StateNameFromSearchResult.Text,
                    ZipCode = ZipCodeFromSearchResult.Text,
                    Latitude = LatitudeFromSearchResult.Text,
                    Longitude = LongitudeFromSearchResult.Text
                });

                googleMapsLinks.Add($"{CityNameFromSearchResult.Text}-{StateNameFromSearchResult.Text}-{ZipCodeFromSearchResult.Text}.jpg", GenerateGoogleMapsLink(LatitudeFromSearchResult.Text, LongitudeFromSearchResult.Text));
                Driver.Navigate().Back();
            }
        }

        public string GenerateGoogleMapsLink(string latitude, string longitude)
        {
            return $"https://maps.google.com/?q={latitude},{longitude}";
        }

        public void TakeScreenshotOfGoogleMapsLinks()
        {
            foreach (var googleLinks in googleMapsLinks)
            {
                Driver.Navigate().GoToUrl(googleLinks.Value);

                Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                screenshot.SaveAsFile(googleLinks.Key, ScreenshotImageFormat.Jpeg);
            }
        }
    }
}
