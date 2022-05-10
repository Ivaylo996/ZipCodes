using OpenQA.Selenium;
using System.Collections.Generic;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage
    {
        public IWebElement AdvancedSearchButton => WaitAndFindElement(By.XPath("//a[contains(text(), 'Advanced Search')]"));
        public IWebElement TownInputTextBox => WaitAndFindElement(By.XPath("//label[contains(text(), 'Town/City:')]//following-sibling::input[@name='fld-city']"));
        public IWebElement FindZipCodesButton => WaitAndFindElement(By.XPath("//input[@value='3']//following-sibling::input[@class='srchButton']"));
        public IWebElement GdprConsentButton => WaitAndFindElement(By.XPath("//button[@aria-label='Consent']"));

        public IList<IWebElement> GetGDPR()
        {
            return Driver.FindElements(By.XPath("//button[@aria-label='Consent']"));
        }
    }
}
