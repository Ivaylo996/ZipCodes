using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        protected abstract string Url { get;}

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

        protected void ScrollToElement(IWebElement iWebElement)
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", iWebElement);
        }

        protected IWebElement WaitAndFindElement(By by)
        {
            return WebDriverWait.Until(ExpectedConditions.ElementExists(by));
        }
    }
}
