using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using ZipCodes.Pages.GoogleMapsPage;
using ZipCodes.Pages.MainPage;
using ZipCodes.Pages.SearchPage;
using ZipCodes.Pages.ZipCodeInfoPage;

namespace ZipCodes
{
    public class ZipCodesTests
    {
        private static EventFiringWebDriver _driver;
        private static MainPage _mainPage;
        private static SearchPage _searchPage;
        private static ZipCodeInfoPage _zipCodeInfoPage;
        private static GoogleMapsPage _googleMapsPage;

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
            _googleMapsPage = new GoogleMapsPage(_driver);
        }

        [SetUp]
        public void Setup()
        {
            _driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TestCleanup()
        {
            WebDriverEventHandler.PerformanceTimingService.GenerateReport();
            _driver.Quit();
        }

        [Test]
        public void ScreenshotCreated_When_CreatesGoogleMapsLinks()
        {
            _mainPage.GoToSearchPage();

            _searchPage.AssertRedirectedToSeachPage("Advanced Search");

            _searchPage.AdvancedSearchZipCodesByCityName("Iva");
            _googleMapsPage.TakeScreenshotOfGoogleMapsLinksAndSaveAsFile(_zipCodeInfoPage.GenerateGoogleMapsLinksByNumberOfCities(5));
        }
    }
}