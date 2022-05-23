using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ZipCodes.Pages.ZipCodeInfoPage
{
    public partial class ZipCodeInfoPage : WebPage
    {
        private List<ZipCodeInformation> zipCodeInfo = new();
        private static Dictionary<string, string> googleMapsLinks = new();

        public ZipCodeInfoPage(IWebDriver _driver) 
            : base(_driver)
        {
        }

        public Dictionary<string, string> GenerateGoogleMapsLinksByNumberOfCities(int numberOfCities)
        {
            List<string> zipCodeInfoLinks = GetResultZipCodeInfoLinks().Select(zipCodes => zipCodes.Text.ToString()).ToList();

            for (int i = 0; i < numberOfCities; i++)
            {              
                Driver.Navigate().GoToUrl(base.Url + $"zip-code/{zipCodeInfoLinks.ElementAt(i)}/zip-code-{zipCodeInfoLinks.ElementAt(i)}.asp");

               zipCodeInfo.Add(new ZipCodeInformation()
                {
                    CityName = CityNameFromSearchResult.Text,
                    StateName = StateNameFromSearchResult.Text,
                    ZipCode = ZipCodeFromSearchResult.Text,
                    Latitude = LatitudeFromSearchResult.Text,
                    Longitude = LongitudeFromSearchResult.Text
                });

                googleMapsLinks.Add($"{CityNameFromSearchResult.Text}-{StateNameFromSearchResult.Text}-{ZipCodeFromSearchResult.Text}.jpg", $"https://maps.google.com/?q={LatitudeFromSearchResult.Text},{LongitudeFromSearchResult.Text}");
            }

            return googleMapsLinks;
        }
    }
}
