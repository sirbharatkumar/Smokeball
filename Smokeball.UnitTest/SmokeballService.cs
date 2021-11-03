using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Smokeball.Common.RequestModel;
using Smokeball.Common.ResponseModel;
using Smokeball.Domain.Services;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Smokeball.UnitTest
{
    [TestClass]
    public class SmokeballService
    {
        GoogleSearchResultsReader googleSearchResultsReader;
        IHtmlDownloader _htmlDownloader;
        //Please change the Mock file folder before running the Unit test
        string htmlMockFilePath = @"..\Smokeball\Smokeball.UnitTest\MockHtml";
        [TestInitialize]
        public void TestInitialize()
        {
            _htmlDownloader = Mock.Of<HtmlDownloader>();
            googleSearchResultsReader = new GoogleSearchResultsReader(_htmlDownloader);
        }

        [TestMethod()]
        public async Task TestSmokeballTrue()
        {
            var path = System.IO.Path.Combine(htmlMockFilePath, "MockFile1.html");

            string html = File.ReadAllText(path);

            SearchResult result = await googleSearchResultsReader.GetSearchResultUsingHtml(html, "www.smokeball.com.au");

            Assert.AreEqual("2", string.Join(", ", result.Rankings.Select(s => s)));
        }


        [TestMethod()]
        public async Task TestSmokeballEmpty()
        {
            var path = System.IO.Path.Combine(htmlMockFilePath, "MockFile2.html");

            string html = File.ReadAllText(path);

            SearchResult result = await googleSearchResultsReader.GetSearchResultUsingHtml(html, "www.dummywebsite.com.au");

            Assert.AreEqual("0", string.Join(", ", result.Rankings.Select(s => s)));
        }

        [TestMethod()]
        public async Task TestSmokeballMultipleTrue()
        {
            var path = System.IO.Path.Combine(htmlMockFilePath, "MockFile3.html");

            string html = File.ReadAllText(path);

            SearchResult result = await googleSearchResultsReader.GetSearchResultUsingHtml(html, "www.smokeball.com.au");

            Assert.AreEqual("2, 7", string.Join(", ", result.Rankings.Select(s => s)));
        }


        [TestMethod()]
        public async Task TestSmokeballClientTrue()
        {
            SearchResult result = await googleSearchResultsReader.GetSearchResult(new SearchRequest {SearchText= "conveyancing software", SearchUrlInResult= "www.smokeball.com.au" });

            Assert.AreEqual(1, result.Rankings.Count());
        }

        [TestMethod()]
        public async Task TestSmokeballClientFalse()
        {
            SearchResult result = await googleSearchResultsReader.GetSearchResult(new SearchRequest { SearchText = "conveyancing software", SearchUrlInResult = "www.test.com.au" });

            Assert.AreEqual("0", string.Join(", ", result.Rankings.Select(s => s)));
        }
    }
}
