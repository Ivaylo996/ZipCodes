using NUnit.Framework;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage
    {
        public void AssertionRedirectedToSeachPage()
        {
            Assert.AreEqual("Advanced Search", AdvancedSearchButton.Text);
        }
    }
}
