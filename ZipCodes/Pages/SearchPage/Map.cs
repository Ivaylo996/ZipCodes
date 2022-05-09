using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage
    {
        public IWebElement advancedSearchButton => WaitAndFindElement(By.XPath("//a[contains(text(), 'Advanced Search')]"));
        public IWebElement townInputTextBox => WaitAndFindElement(By.XPath("//label[contains(text(), 'Town/City:')]//following-sibling::input[@name='fld-city']"));
        public IWebElement findZipCodesButton => WaitAndFindElement(RelativeBy.WithLocator(By.XPath("//input[@class='srchButton' and @value='Find ZIP Codes']")).Below(By.XPath("//label[contains(text(), 'Area Code:')]//following-sibling::input[@name='fld-areacode']")));
        public IWebElement actualPageCounterLabel => WaitAndFindElement(By.XPath("//div[@style='padding:20px; text-align:right; font-size:13px;' and contains(text(),'Page 1 of 1')]"));
        public IWebElement gdprConsentButton => WaitAndFindElement(By.XPath("//button[@aria-label='Consent']"));
        public IWebElement zipCodeFromSearchResult => WaitAndFindElement(RelativeBy.WithLocator(By.XPath("//td[@class='info']")).RightOf(By.XPath("//span[contains(text(),'Zip Code:')]")));
        public IWebElement cityNameFromSearchResult => WaitAndFindElement(RelativeBy.WithLocator(By.XPath("//td[@class='info']")).RightOf(By.XPath("//span[contains(text(),'City:')]")));
        public IWebElement stateNameFromSearchResult => WaitAndFindElement(RelativeBy.WithLocator(By.XPath("//td[@class='info']")).RightOf(By.XPath("//span[contains(text(),'State:')]")));
        public IWebElement longitudeFromSearchResult => WaitAndFindElement(RelativeBy.WithLocator(By.XPath("//td[@class='info']")).RightOf(By.XPath("//span[contains(text(),'Longitude:')]")));
        public IWebElement latitudeFromSearchResult => WaitAndFindElement(RelativeBy.WithLocator(By.XPath("//td[@class='info']")).RightOf(By.XPath("//span[contains(text(),'Latitude:')]")));
        public IWebElement GetZipCodesInfoButton(int index)
        {
            if (index >=2 && index <= 11)
            {
                return WaitAndFindElement(By.XPath($"//tbody//tr[{index}]//a[contains(@href,'/zip-code/')]"));
            }
            else
            {
                throw new Exception("Index out of bounds");
            }
        }
    }
}
