using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZipCodes.Pages.MainPage
{
    public partial class MainPage : WebPage
    {
        public MainPage(IWebDriver _driver) : base(_driver)
        {
        }

        protected override string Url => "https://www.zip-codes.com/";

        public void GoToSearchPage()
        {
            GoTo();

            searchButton.Click();
        }
    }
}
