using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Text.Json;                                                                //11.01.2021 TMLINARIC Needed for json parsing
using Turtl_TMlinaric.Models;                                                          //10.01.2021 TMLINARIC References the definition of "Post" element
using Turtl_TMlinaric.Shared;                                                          //11.01.2021 TMLINARIC Code repetition is bad, m'kaaaaay?


namespace Turtl_TMlinaric.Pages
{
    public class PostDetails : PageModel
    {
        private readonly ILogger<PostDetails> _logger;

        public PostDetails(ILogger<PostDetails> logger)
        {
            _logger = logger;
        }

        public int selectedPostID { get; set; }
        public Post selectedPost { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            selectedPostID = id;

            string stringJsonPosts = "";
            try
            {
                stringJsonPosts = await Functions.GetJsonPostsFromUrlAsString("https://jsonplaceholder.typicode.com/posts", _logger);
            }
            catch
            {
                _logger.LogError("Something went wrong with fucntion 'GetJsonPostsFromUrlAsString!'");
                TempData["ErrorMessage"] = "could not load json from source";

                return Page();
            }

            if (stringJsonPosts != "ER1" || stringJsonPosts != "ER2" || stringJsonPosts != "ER3" || stringJsonPosts != "ER4")
            {
                var listOfPosts = new List<Post>();
                try
                {
                    listOfPosts = JsonSerializer.Deserialize<List<Post>>(stringJsonPosts);
                }
                catch
                {
                    _logger.LogError("Could not deserialize json format from source");
                    TempData["ErrorMessage"] = "json data format incorrect";
                    return Page();
                }

                foreach (Post post in listOfPosts)
                {
                    if (post.postID == selectedPostID)
                    {
                        selectedPost = post;
                        break;
                    }
                }
                return Page();
            }
            else
            {
                switch (stringJsonPosts)
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
                        return Page();
                }
            }
        }
    }
}
