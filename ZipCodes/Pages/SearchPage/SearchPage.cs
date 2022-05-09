using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage : WebPage
    {
        public SearchPage(IWebDriver _driver) : base(_driver)
        {
        }

        protected override string Url => throw new NotImplementedException();

        private List<ZipCodeInfo> zipCodeInfo = new ();

        public void AdvancedSearchZipCodes(string cityName)
        {
            WaitForPageToLoad();

            if (gdprConsentButton.Displayed)
            {
                gdprConsentButton.Click();
            }

            advancedSearchButton.Click();
            townInputTextBox.SendKeys(cityName);
            findZipCodesButton.Click();
        }

        public void GetCityInfo()
        {
            for (int i = 2; i <= 11; i++)
            {
                GetZipCodesInfoButton(i).Click();

                zipCodeInfo.Add(new ZipCodeInfo() 
                { 
                    CityName = cityNameFromSearchResult.Text,
                    StateName = stateNameFromSearchResult.Text,
                    ZipCode = zipCodeFromSearchResult.Text,
                    Latitude = latitudeFromSearchResult.Text,
                    Longitude = longitudeFromSearchResult.Text
                });

                TakeScreenshotOfGoogleMapsLink(cityNameFromSearchResult.Text, stateNameFromSearchResult.Text, zipCodeFromSearchResult.Text, latitudeFromSearchResult.Text, longitudeFromSearchResult.Text);
            }
        }

        public string GenerateGoogleMapsLink(string latitude, string longitude)
        {
            return $"https://maps.google.com/?q={latitude},{longitude}";
        }

        public void TakeScreenshotOfGoogleMapsLink(string city, string state, string zipCode, string latitude, string longitude)
        {
            string fileName = $"{city}-{state}-{zipCode}.jpg";

            Driver.Navigate().GoToUrl(GenerateGoogleMapsLink(latitude,longitude));

            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);

            Driver.Navigate().Back();
            Driver.Navigate().Back();
        }

        protected override void WaitForPageToLoad()
        {
            WaitAndFindElement(By.XPath("//button[@aria-label='Consent']"));
        }
    }
}
