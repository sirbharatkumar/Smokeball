using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Smokeball.Common.RequestModel;
using Smokeball.Common.ResponseModel;
using Smokeball.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Smokeball.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebScrapperController : ControllerBase
    {

        #region Private Members
        private readonly IGoogleSearchResultsReader _googleSearchResultsReader;

        private readonly ILogger<WebScrapperController> _logger;
        #endregion

        public WebScrapperController(IGoogleSearchResultsReader googleSearchResultsReader, ILogger<WebScrapperController> logger)
        {
            _googleSearchResultsReader = googleSearchResultsReader;
            _logger = logger;
        }

        /// <summary>
        /// Determine the ranking for a given web url within the google search result
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     {
        ///         "searchText":"conveyancing software",
        ///         "searchUrlInResult":"www.smokeball.com.au"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Search text and Search Url</param>
        /// <returns>Result showing the web url ranking and html snippet of the seach result</returns>
        /// <response code="200">Returns the success response</response>
        /// <response code="400">If the input is not valid</response>  
        /// <response code="401">Unauthorized Access can't proceed further</response>  
        [HttpPost]
        [Route("~/api/search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces("application/json", Type = typeof(SearchResult))]
        public async Task<ActionResult> GetWebsiteRanking([FromBody] SearchRequest request)
        {
            try
            {
                _logger.LogInformation("GetWebsiteRanking Request: {0}", request.ToString());
                SearchResult response =  await _googleSearchResultsReader.GetSearchResult(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception inside GetWebsiteRanking Error: {0}", ex);
                return StatusCode(500, ex);
            }
        }
    }
}
