using OpenQA.Selenium;

namespace ZipCodes.Pages.MainPage
{
    public partial class MainPage : WebPage
    {
        public MainPage(IWebDriver _driver) : base(_driver)
        {
        }

        public void GoToSearchPage()
        {
            GoTo();

            MainPageSearchButton.Click();
        }
    }
}
