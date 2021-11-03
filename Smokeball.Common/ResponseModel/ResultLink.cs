using System;
using System.Collections.Generic;
using System.Text;

namespace Smokeball.Common.ResponseModel
{
    /// <summary>
    /// Result having the order and html snippet of the search result
    /// </summary>
    public class ResultLink
    {
        public int Order { get; set; }        
        public string Html { get; set; }
    }
}
