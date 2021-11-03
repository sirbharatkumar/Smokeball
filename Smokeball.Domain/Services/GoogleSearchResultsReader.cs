using Smokeball.Common.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
using Smokeball.Common.RequestModel;
using Smokeball.Common.Constants;

namespace Smokeball.Domain.Services
{
    public class GoogleSearchResultsReader : IGoogleSearchResultsReader, IDisposable
    {
        private SearchResult _searchResult;

        private bool _disposed = false;

        private readonly IHtmlDownloader _htmlDownloader;

        /// <summary>
        /// Constructore
        /// </summary>
        /// <param name="htmlDownloader"></param>
        public GoogleSearchResultsReader(IHtmlDownloader htmlDownloader)
        {
            _searchResult = new SearchResult();
            _htmlDownloader = htmlDownloader;
        }

        ~GoogleSearchResultsReader() => Dispose(false);

        /// <summary>
        /// Scrape the Google result with the requested search text and find the ranking of the requested url.
        /// </summary>
        /// <param name="request">SearchText and SearchUrlInResult</param>
        /// <returns>Ranking and html snippet</returns>
        public async Task<SearchResult> GetSearchResult(SearchRequest request)
        {
            //Prepare the search URL using the search text coming in request. Google URL and Num can we configurable outside code
            string url = $"{ApplicationConstants.GoogleSearchUrl}?q={request.SearchText}&num={ApplicationConstants.NumberOfResults}";

            //Call the HTML downloader and get the raw html
            var rawHtml = await _htmlDownloader.GetContentAsync(url);

            //Scrape the search result content with the regex pattern and prepare a list
            this.GetSearchResultsList(rawHtml);

            //Determine the ranking from the prepared list
            this.GetRankings(request.SearchUrlInResult);

            return _searchResult;
        }

        private void GetSearchResultsList(string html)
        {
            //Regex pattern to scrape the content
            Regex searchResultRegex = new Regex(RegexPattern.SearchResultPattern);

            //Gives the list of matched html div which contains the anchor url which will determine the ranking
            var matches = searchResultRegex.Matches(html);

            //Prepare the resultlink list based on the matched collection
            _searchResult.ResultLinks = matches.Select((s, i) => new ResultLink { Html = s.Value, Order = i + 1 }).ToList();
        }

        private void GetRankings(string searchUrlInResult)
        {
            //Get the ranking for the requested Url from the match collections
            _searchResult.Rankings = _searchResult.ResultLinks
                                                    .Where(x => x.Html.Contains(searchUrlInResult))
                                                    .Select(x => x.Order).ToList();
            if (_searchResult.Rankings == null || _searchResult.Rankings.Count() <= 0)
            {
                _searchResult.Rankings = new List<int>();
                _searchResult.Rankings.Add(0);
            }

        }

        /// <summary>
        /// Get SearchResult based on the raw HTML and search URL
        /// </summary>
        /// <param name="html"></param>
        /// <param name="searchUrlInResult"></param>
        /// <returns></returns>
        public async Task<SearchResult> GetSearchResultUsingHtml(string html, string searchUrlInResult)
        {
            //Scrape the search result content with the regex pattern and prepare a list
            this.GetSearchResultsList(html);

            //Determine the ranking from the prepared list
            this.GetRankings(searchUrlInResult);

            return _searchResult;
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed objects that implement IDisposable.
                // Assign null to managed objects that consume large amounts of memory or consume scarce resources.
            }

            // Free unmanaged resources (unmanaged objects).

            _disposed = true;
        }
    }
}
