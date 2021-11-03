using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Smokeball.Domain.Services
{
    public interface IHtmlDownloader
    {
        /// <summary>
        /// Get the HTML for the given URL
        /// </summary>
        /// <param name="searchUrl">Search URL</param>
        /// <returns>HTML content returned for the provided URL</returns>
        Task<string> GetContentAsync(string searchUrl);
    }
}
