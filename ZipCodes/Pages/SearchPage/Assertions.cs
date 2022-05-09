using NUnit.Framework;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage
    {
        public void AssertionZipCodesInfoDisplayed_When_AdvancedSearchForTown(string expectedLabelText)
        {
            Assert.AreEqual(expectedLabelText ,actualPageCounterLabel.Text.Trim());
        }

        public void AssertionRedirectedToSeachPage_When_ClickSearchButton()
        {
            Assert.AreEqual("Advanced Search", advancedSearchButton.Text);
        }
    }
}
