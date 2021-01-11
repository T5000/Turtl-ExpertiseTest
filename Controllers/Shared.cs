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
using Turtl_TMlinaric.Models;                                                          //10.01.2021 TMLINARIC References the definition of "Message" element

namespace Turtl_TMlinaric.Shared
{
    public static class Functions
    {
        public static async Task<string> GetJsonMessagesFromUrlAsString(string url)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string stringJsonMessages;
                stringJsonMessages = await response.Content.ReadAsStringAsync();
                return stringJsonMessages;
            }
            else
            {
                //TODO:Log the error
                return null;
            }
        }
    }
}