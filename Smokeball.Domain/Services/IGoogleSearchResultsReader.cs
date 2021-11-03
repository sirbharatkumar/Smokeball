using Smokeball.Common.RequestModel;
using Smokeball.Common.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball.Domain.Services
{
    public interface IGoogleSearchResultsReader
    {
        /// <summary>
        /// Scrape the Google result with the requested search text and find the ranking of the requested url.
        /// </summary>
        /// <param name="request">SearchText and SearchUrlInResult</param>
        /// <returns>Ranking and html snippet</returns>
        Task<SearchResult> GetSearchResult(SearchRequest request);
    }
}
