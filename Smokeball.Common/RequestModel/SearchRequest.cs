using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Smokeball.Common.RequestModel
{
    /// <summary>
    /// Search request model 
    /// </summary>
    public class SearchRequest
    {
        public SearchRequest()
        {
            SearchText = string.Empty;
            SearchUrlInResult = string.Empty;
        }

        [Required]
        [RegularExpression("([a-zA-Z0-9 ]+)", ErrorMessage = "Invalid Search Text. It can only contain alphabets, number and space.")]
        public string SearchText { get; set; }

        [Required]
        public string SearchUrlInResult { get; set; }

        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SearchRequest =>");
                sb.Append(string.Format("SearchText: {0}",SearchText));
                sb.Append(string.Format("SearchUrlInResult: {0}", SearchUrlInResult));
                return sb.ToString();
            }
            catch
            {
                return "Error occurred while generating the ToString for SearchRequest";
            }
        }
    }
}
