using OpenQA.Selenium;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage : WebPage
    {
        private string currentSubUrl = "search.asp";

        public SearchPage(IWebDriver _driver) : base(_driver)
        {
        }

        protected override string Url
        { 
            get
            {
                return base.Url + currentSubUrl;
            }
        }

        public void AdvancedSearchZipCodesByCityName(string cityName)
        {
            WaitUntilPageLoadsCompletely();

            Driver.Manage().Cookies.AddCookie(new Cookie("complianz_consent_status", "allow"));
            Driver.Navigate().Refresh();

            SearchPageAdvancedSearchButton.Click();
            SearchPageTownInputTextBox.SendKeys(cityName);
            SearchPageFindZipCodesButton.Click();
        }
    }
}
