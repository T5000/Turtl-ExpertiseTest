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
    public class InteractiveIndexModel : PageModel
    {
        private readonly ILogger<InteractiveIndexModel> _logger;
        public InteractiveIndexModel(ILogger<InteractiveIndexModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {

        }
    }
}