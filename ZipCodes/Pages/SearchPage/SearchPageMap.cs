using OpenQA.Selenium;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage
    {
        public IWebElement AdvancedSearchButton => Driver.FindElement(By.XPath("//a[contains(text(), 'Advanced Search')]"));
        public IWebElement TownInputTextBox => Driver.FindElement(By.XPath("//label[contains(text(), 'Town/City:')]//following-sibling::input[@name='fld-city']"));
        public IWebElement FindZipCodesButton => Driver.FindElement(By.XPath("//input[@value='3']//following-sibling::input[@class='srchButton']"));
    }
}
