using Smokeball.Common;
using Smokeball.Common.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball.Domain.Services
{
    public class HtmlDownloader : IHtmlDownloader
    {
        /// <summary>
        /// Get the HTML for the given URL
        /// </summary>
        /// <param name="searchUrl">Search URL</param>
        /// <returns>HTML content returned for the provided URL</returns>
        public async Task<string> GetContentAsync(string searchUrl)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                    client.DefaultRequestHeaders.Accept.Clear();

                    //Get the content for the specific URL
                    var response = await client.GetStringAsync(searchUrl);
                    if (string.IsNullOrEmpty(response))
                    {
                        throw new NoResponseException(ExceptionMessages.NoResponseException);
                    }

                    return response;
                }
            }
            catch (Exception)
            {
                throw new ClientException(ExceptionMessages.ClientException);
            }
        }
    }
}
