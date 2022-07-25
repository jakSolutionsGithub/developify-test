using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace client.Pages
{
    public class Contact : PageModel
    {
        [BindProperty]
        public Models.ContactModel ContactModel { get; set; }
        private readonly ILogger<Contact> _logger;

        public Contact(ILogger<Contact> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}