using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Text.Json;                                                                //10.01.2021 TMLINARIC Needed for json parsing
using Turtl_TMlinaric.Models;                                                          //10.01.2021 TMLINARIC References the definition of "Message" element
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
        public List<Message> listOfMessages { get; set; }

        public async Task OnGet()
        {            
            listOfMessages = new List<Message>();
            string stringJsonMessages = await Functions.GetJsonMessagesFromUrlAsString("https://jsonplaceholder.typicode.com/posts");
            
            if (stringJsonMessages != null)
            {
                listOfMessages = JsonSerializer.Deserialize<List<Message>>(stringJsonMessages);
            }
            else
            {
                //TODO:Return null content error
                return;
            }
        }
    }
}
