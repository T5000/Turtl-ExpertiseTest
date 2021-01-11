using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using System.Text.Json;                                                                //11.01.2021 TMLINARIC Needed for json parsing
using Turtl_TMlinaric.Models;                                                          //10.01.2021 TMLINARIC References the definition of "Message" element
using Turtl_TMlinaric.Shared;                                                          //11.01.2021 TMLINARIC Code repetition is bad, m'kaaaaay?


namespace Turtl_TMlinaric.Pages
{
    public class MessageDetails : PageModel
    {
        private readonly ILogger<MessageDetails> _logger;

        public MessageDetails(ILogger<MessageDetails> logger)
        {
            _logger = logger;
        }

        public int selectedPostID { get; set; }
        public Message selectedMessage { get; set; }
        //public List<Message> listOfMessages { get; set; }
        

        public async Task<IActionResult> OnGet(int id)
        {
            selectedPostID = id;
            string stringJsonMessages = await Functions.GetJsonMessagesFromUrlAsString("https://jsonplaceholder.typicode.com/posts");

            if (stringJsonMessages != null)
            {
                var listOfMessages = JsonSerializer.Deserialize<List<Message>>(stringJsonMessages);
                foreach (Message message in listOfMessages)
                {
                    if (message.postID == selectedPostID)
                    {
                        selectedMessage = message;
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
