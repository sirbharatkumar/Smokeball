using System;
using System.Collections.Generic;
using System.Text;

namespace Smokeball.Common.Constants
{
    /// <summary>
    /// Constants used throughout the application
    /// </summary>
    public static class ApplicationConstants
    {
        public static int ErrorValue => -999;
        public static string GoogleSearchUrl => "https://www.google.com.au/search";
        public static int NumberOfResults => 100;
    }

    /// <summary>
    /// Regex pattern to scrape the html content
    /// </summary>
    public static class RegexPattern
    {
        public static string SearchResultPattern => "<div class=\"kCrYT\"><a href=\"/url?(.*?)</h3>(.*?)</div></a></div>";
    }

    /// <summary>
    /// Exception messages for generic scenarios
    /// </summary>
    public static class ExceptionMessages
    {
        public const string ClientException = "Error occured while fetching data from Client";
        public const string NoResponseException = "No response was returned from the Client";
        public const string ApplicationException = "Error occured while processing the data";
    }
}
