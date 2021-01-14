using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Net.Http;                                                                 //09.01.2021 TMLINARIC Needed by HttpClient
using System.Text.Json;                                                                //10.01.2021 TMLINARIC Needed for json parsing
using System.Text.Json.Serialization;
using Turtl_TMlinaric.Models;                                                          //10.01.2021 TMLINARIC References the definition of "Post" element

using System.Diagnostics;

namespace Turtl_TMlinaric.Shared
{
    public static class Functions
    {
        public static async Task<string> GetJsonPostsFromUrlAsString(string url, ILogger _logger)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            
            try
            {
                response = await client.GetAsync(url);
            }
            catch
            {
                _logger.LogInformation(string.Format("No response from provided URL ({0})", url));
                return "ER1";
            }
            
            if (response.IsSuccessStatusCode)
            {
                string stringJsonPosts = null;
                
                try
                {
                    stringJsonPosts = await response.Content.ReadAsStringAsync();
                }
                catch
                {
                    _logger.LogError("Error converting response content to string from source URL {0}", url);
                    return "ER2";
                }

                if (stringJsonPosts != null && stringJsonPosts != "")
                {
                    return stringJsonPosts;
                }
                else
                {
                    _logger.LogWarning("Response from {0} was successfull, but empty", url);
                    return "ER3";
                }
            }
            else
            {
                _logger.LogInformation(string.Format("Provided URL ({0}) respond with status code {1}", url, response.StatusCode.ToString()));
                return "ER4";
            }
        }
    }
}