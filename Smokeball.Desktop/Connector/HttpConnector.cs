using log4net;
using Newtonsoft.Json;
using Smokeball.Common.RequestModel;
using Smokeball.Common.ResponseModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Smokeball.Desktop.Connector
{
    public class HttpConnector : IDisposable
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(HttpConnector));

        private bool _disposed = false;

        public HttpConnector()
        {

        }

        ~HttpConnector() => Dispose(false);

        /// <summary>
        /// Call the Web Search API and get the Search Result based on search parameters
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchUrlInResult"></param>
        /// <returns></returns>
        public SearchResult GetSearchResult(string searchText,string searchUrlInResult)
        {
            _log.InfoFormat("GetSearchResult Parameter SearchText: {0}; SearchUrlInResult: {1}", searchText, searchUrlInResult);

            SearchResult searchResult = new SearchResult();
            try
            {
                using (var client = new HttpClient())
                {
                    //Prepare the request
                    SearchRequest request = new SearchRequest() { SearchText = searchText, SearchUrlInResult = searchUrlInResult };

                    //Add the Authentication token to the http client API call, will use the encrypted appsettings 
                    client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue(
                                "Basic",
                                Convert.ToBase64String(
                                    System.Text.Encoding.ASCII.GetBytes(
                                        string.Format("{0}:{1}", ConfigurationManager.AppSettings.Get("ApiUsername").ToString(), ConfigurationManager.AppSettings.Get("ApiPassword").ToString()))));

                    //Set the base API address
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("ApiBaseAddress").ToString());

                    //Call the API and get the search result
                    var response = client.PostAsJsonAsync(ConfigurationManager.AppSettings.Get("SearchApiUri").ToString(), request).Result;

                    //Check if the status is 200
                    if (response.IsSuccessStatusCode)
                    {

                        StringBuilder sb = new StringBuilder();
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        //Deserialize into SearchResult object
                        searchResult = JsonConvert.DeserializeObject<SearchResult>(responseContent);

                        _log.InfoFormat("GetSearchResult Completed");
                    }
                    else
                    {
                        _log.Error("GetSearchResult Failed to connect API");
                        throw new Exception("Failed to connect Search API !!");
                    }
                }

                return searchResult;
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("GetSearchResult Exception: {0}", ex);
                throw ex;
            }
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
