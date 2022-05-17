using System.Collections.Generic;
using OpenQA.Selenium;

namespace ZipCodes.Pages.ZipCodeInfoPage
{
    public partial class ZipCodeInfoPage
    {
        public IWebElement ZipCodeFromSearchResult => Driver.FindElement(By.XPath("//span[contains(text(), 'Zip Code')]//parent::td//following-sibling::td"));
        public IWebElement CityNameFromSearchResult => Driver.FindElement(By.XPath("//span[contains(text(), 'City')]//parent::td//following-sibling::td//a"));
        public IWebElement StateNameFromSearchResult => Driver.FindElement(By.XPath("//span[contains(text(), 'State')]//parent::td//following-sibling::td//a"));
        public IWebElement LongitudeFromSearchResult => Driver.FindElement(By.XPath("//span[contains(text(), 'Longitude')]//parent::td//following-sibling::td"));
        public IWebElement LatitudeFromSearchResult => Driver.FindElement(By.XPath("//span[contains(text(), 'Latitude')]//parent::td//following-sibling::td"));

        public IList<IWebElement> GetCollectionOfZipCodeInformationLinksFromResultTable()
        {
            return (IList<IWebElement>)Driver.FindElements(By.XPath("//tbody//tr//a[contains(@href,'/zip-code/')]"));
        }
    }
}
