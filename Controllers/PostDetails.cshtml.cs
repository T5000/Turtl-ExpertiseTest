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
            string stringJsonPosts = await Functions.GetJsonPostsFromUrlAsString("https://jsonplaceholder.typicode.com/posts");

            if (stringJsonPosts != null)
            {
                var listOfPosts = JsonSerializer.Deserialize<List<Post>>(stringJsonPosts);
                foreach (Post post in listOfPosts)
                {
                    if (post.postID == selectedPostID)
                    {
                        selectedPost = post;
                    }
                }
            }
            else
            {
                //TODO LOG ERROR
            }

            return Page();
        }
    }
}
