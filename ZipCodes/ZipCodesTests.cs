using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using ZipCodes.Pages.MainPage;
using ZipCodes.Pages.SearchPage;
using ZipCodes.Pages.ZipCodeInfoPage;

namespace ZipCodes
{
    public class ZipCodesTests : IDisposable
    {
        private static EventFiringWebDriver _driver;
        private static MainPage _mainPage;
        private static SearchPage _searchPage;
        private static ZipCodeInfoPage _zipCodeInfoPage; 

        public ZipCodesTests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new EventFiringWebDriver(new ChromeDriver());

            _driver.Navigated += WebDriverEventHandler.FiringDriver_Navigated;
            _driver.Navigating += WebDriverEventHandler.FiringDriver_Navigating;
            _driver.ElementClicking += WebDriverEventHandler.FiringDriver_Clicking;
            _driver.ElementClicked += WebDriverEventHandler.FiringDriver_Clicked;
            _driver.ScriptExecuting += WebDriverEventHandler.FiringDriver_ScriptExecuting;
            _driver.ScriptExecuted += WebDriverEventHandler.FiringDriver_ScriptExecuted;
            _driver.ExceptionThrown += WebDriverEventHandler.FiringDriver_ExceptionThrown;
            _driver.FindingElement += WebDriverEventHandler.FiringDriver_FindingElement;

            _mainPage = new MainPage(_driver);
            _searchPage = new SearchPage(_driver);
            _zipCodeInfoPage = new ZipCodeInfoPage(_driver);
        }

        [SetUp]
        public void Setup()
        {
            _driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [TearDown]
        public void TestCleanup()
        {
            WebDriverEventHandler.PerformanceTimingService.GenerateReport();
        }

        [Test]
        [TestCase("Iva")]
        [TestCase("Dim")]
        public void ScreenshotCreated_When_CreatesGoogleMapsLinkForNumberOfTowns(string cityName)
        {
            _mainPage.GoToSearchPage();

            _searchPage.AssertRedirectedToSeachPage("Advanced Search");

            _searchPage.AdvancedSearchZipCodesByCityName(cityName);
            _zipCodeInfoPage.GetInformationForNumberOfCities(5);
            _zipCodeInfoPage.TakeScreenshotOfGoogleMapsLinksAndSaveAsFile();
        }
    }
}