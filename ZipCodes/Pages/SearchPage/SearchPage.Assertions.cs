using NUnit.Framework;

namespace ZipCodes.Pages.SearchPage
{
    public partial class SearchPage
    {
        public void AssertRedirectedToSeachPage()
        {
            Assert.AreEqual("Advanced Search", AdvancedSearchButton.Text);
        }
    }
}
