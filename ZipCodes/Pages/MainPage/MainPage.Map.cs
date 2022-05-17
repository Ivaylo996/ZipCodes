using OpenQA.Selenium;

namespace ZipCodes.Pages.MainPage
{
    public partial class MainPage
    {
        public IWebElement MainPageSearchButton => Driver.FindElement(By.XPath("//a[@title='FREE ZIP Code Search']"));
    }
}
