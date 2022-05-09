
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using ZipCodes.Pages.MainPage;
using ZipCodes.Pages.SearchPage;

namespace ZipCodes
{
    public class Tests : IDisposable
    {
        private static IWebDriver _driver;
        private static MainPage _mainPage;
        private static SearchPage _searchPage;

        public Tests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new ChromeDriver();
            _mainPage = new MainPage(_driver);
            _searchPage = new SearchPage(_driver);
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

        [Test]
        public void ScreenshotCreated_When_CreatesGoogleMapsLinkForFirstTenTown([Values("Iva", "Dim")]string cityName)
        {
            _mainPage.GoToSearchPage();
            _searchPage.AssertionRedirectedToSeachPage_When_ClickSearchButton();
            _searchPage.AdvancedSearchZipCodes(cityName);
            _searchPage.GetCityInfo();
        }
    }
}