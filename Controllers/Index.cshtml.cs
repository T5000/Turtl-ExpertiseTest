using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Text.Json;                                                                //10.01.2021 TMLINARIC Needed for json parsing
using Turtl_TMlinaric.Models;                                                          //10.01.2021 TMLINARIC References the definition of "Post" element
using Turtl_TMlinaric.Shared;                                                          //11.01.2021 TMLINARIC Code repetition is bad, m'kaaaaay?

namespace Turtl_TMlinaric.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public List<Post> listOfPosts { get; set; }

        public async Task<IActionResult> OnGet()
        {
            listOfPosts = new List<Post>();
            string JsonPostsAsString = "";
            try
            {
                JsonPostsAsString = await Functions.GetJsonPostsFromUrlAsString("https://jsonplaceholder.typicode.com/posts", _logger);
            }
            catch
            {
                _logger.LogError("Something went wrong with fucntion 'GetJsonPostsFromUrlAsString!'");
                TempData["ErrorMessage"] = "could not load json from source";
                return Page();
            }
            
            switch (JsonPostsAsString)
            {
                case "ER1":
                    TempData["ErrorMessage"] = "No response from source URL";
                    return Page();
                case "ER2":
                    TempData["ErrorMessage"] = "Error converting response content to string from source";
                    return Page();
                case "ER3":
                    TempData["ErrorMessage"] = "Empty page content";
                    return Page();
                case "ER4":
                    TempData["ErrorMessage"] = "Unsuccessful communication with source";
                    return Page();
                default:
                    try 
                    {
                        listOfPosts = JsonSerializer.Deserialize<List<Post>>(JsonPostsAsString);
                        return Page();
                    }
                    catch
                    {
                        _logger.LogError("Could not deserialize json format from source");
                        TempData["ErrorMessage"] = "json data format incorrect";
                        return Page();
                    }
            }
        }
    }
}
