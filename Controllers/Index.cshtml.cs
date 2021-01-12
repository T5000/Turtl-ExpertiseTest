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

        public async Task OnGet()
        {
            listOfPosts = new List<Post>();
            string JsonPostsAsString = await Functions.GetJsonPostsFromUrlAsString("https://jsonplaceholder.typicode.com/posts");
            
            if (JsonPostsAsString != null)
            {
                listOfPosts = JsonSerializer.Deserialize<List<Post>>(JsonPostsAsString);
            }
            else
            {
                //TODO:Return null content error
                return;
            }
        }
    }
}
