using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace client.Pages
{
    public class Services : PageModel
    {
        private readonly ILogger<Services> _logger;

        public Services(ILogger<Services> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}