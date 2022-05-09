using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace ZipCodes.Pages.MainPage
{
    public partial class MainPage
    {
        public IWebElement searchButton => WaitAndFindElement(By.XPath("//a[@title='FREE ZIP Code Search']"));

    }
}
