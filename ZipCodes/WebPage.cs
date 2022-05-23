using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;

namespace ZipCodes
{
    public abstract class WebPage
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 60;

        public WebPage(IWebDriver _driver)
        {
            Driver = _driver;
            WebDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
        }

        protected IWebDriver Driver { get; set; }
        protected WebDriverWait WebDriverWait { get; set; }
        protected virtual string Url => "https://www.zip-codes.com/";

        public void GoTo()
        {
            Driver.Navigate().GoToUrl(Url);
            WaitForPageToLoad();
        }
        
        protected void HoverElement(IWebElement iWebElement)   
        {
            var actions = new Actions(Driver);

            actions.MoveToElement(iWebElement).Perform();
        }

        protected virtual void WaitForPageToLoad()
        {
        }

        protected void ScrollToElement(IWebElement elementToScrollTo)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", elementToScrollTo);
        }

        public void WaitUntilElementIsClickable(IWebElement elementToClick)
        {
            WebDriverWait.Until(ExpectedConditions.ElementToBeClickable(elementToClick));
        }

        protected IWebElement WaitAndFindElement(By by)
        {
            return WebDriverWait.Until(ExpectedConditions.ElementExists(by));
        }

        public void WaitForAjax()
        {
            var js = (IJavaScriptExecutor)Driver;
            WebDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }

        public void WaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)Driver;
            WebDriverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }
    }
}
