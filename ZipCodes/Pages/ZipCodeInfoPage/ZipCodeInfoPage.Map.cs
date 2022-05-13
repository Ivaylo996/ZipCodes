using System.Collections.Generic;
using OpenQA.Selenium;

namespace ZipCodes.Pages.ZipCodeInfoPage
{
    public partial class ZipCodeInfoPage
    {
        public IWebElement ZipCodeFromSearchResult => WaitAndFindElement(By.XPath("//span[contains(text(), 'Zip Code')]//parent::td//following-sibling::td"));
        public IWebElement CityNameFromSearchResult => WaitAndFindElement(By.XPath("//span[contains(text(), 'City')]//parent::td//following-sibling::td//a"));
        public IWebElement StateNameFromSearchResult => WaitAndFindElement(By.XPath("//span[contains(text(), 'State')]//parent::td//following-sibling::td//a"));
        public IWebElement LongitudeFromSearchResult => WaitAndFindElement(By.XPath("//span[contains(text(), 'Longitude')]//parent::td//following-sibling::td"));
        public IWebElement LatitudeFromSearchResult => WaitAndFindElement(By.XPath("//span[contains(text(), 'Latitude')]//parent::td//following-sibling::td"));

        public IList<IWebElement> CollectAllLinks()
        {
            return (IList<IWebElement>)Driver.FindElements(By.XPath("//tbody//tr//a[contains(@href,'/zip-code/')]"));
        }
    }
}
