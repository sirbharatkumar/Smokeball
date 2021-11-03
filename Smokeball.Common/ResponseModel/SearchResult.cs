using System;
using System.Collections.Generic;
using System.Text;

namespace Smokeball.Common.ResponseModel
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchResult
    {
        public SearchResult()
        {
            Rankings = new List<int>();
            ResultLinks = new List<ResultLink>();
        }

        public List<int> Rankings { get; set; }
        public List<ResultLink> ResultLinks { get; set; }
    }
}
