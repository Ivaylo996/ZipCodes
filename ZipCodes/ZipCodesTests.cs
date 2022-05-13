using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using ZipCodes.Pages.ZipCodeInfoPage;
using ZipCodes.Pages.MainPage;
using ZipCodes.Pages.SearchPage;
using OpenQA.Selenium.Support.Events;

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
        public void ScreenshotCreated_When_CreatesGoogleMapsLinkForFirstTenTown(string cityName)
        {
            _mainPage.GoToSearchPage();

            _searchPage.AssertionRedirectedToSeachPage();

            _searchPage.AdvancedSearchZipCodes(cityName);
            _zipCodeInfoPage.GetCityInfo(5);
            _zipCodeInfoPage.TakeScreenshotOfGoogleMapsLinks();
        }
    }
}