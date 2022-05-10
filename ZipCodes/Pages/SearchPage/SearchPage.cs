﻿using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

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

            if (GetGDPR().Count != 0)
            {
                WaitUntilElementIsClickable(GdprConsentButton);
                GdprConsentButton.Click();
            }

            AdvancedSearchButton.Click();
            TownInputTextBox.SendKeys(cityName);
            FindZipCodesButton.Click();

            WaitForAjax();
        }

        private void WaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)Driver;
            WebDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }
    }
}
