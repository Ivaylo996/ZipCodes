using OpenQA.Selenium;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage : WebPage
    {
        public SearchPage(IWebDriver _driver) : base(_driver)
        {
        }

        protected override string Url => "https://www.zip-codes.com/search.asp";

        public void AdvancedSearchZipCodes(string cityName)
        {
            WaitUntilPageLoadsCompletely();

            Driver.Manage().Cookies.AddCookie(new Cookie("complianz_consent_status", "allow"));
            Driver.Navigate().Refresh();

            AdvancedSearchButton.Click();
            TownInputTextBox.SendKeys(cityName);
            FindZipCodesButton.Click();

            WaitForAjax();
        }
    }
}
