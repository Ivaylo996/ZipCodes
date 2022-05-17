using NUnit.Framework;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage
    {
        public void AssertRedirectedToSeachPage(string expectedSearchButtonText)
        {
            Assert.AreEqual(expectedSearchButtonText, SearchPageAdvancedSearchButton.Text);
        }
    }
}
