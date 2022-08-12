using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace client.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    [HttpPost]
    public IActionResult OnPostCultureManagementAsync(string culture, string returnUrl)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

        var result = returnUrl.IndexOf("/", 2);
        if (result == -1)
        {
            returnUrl = "~/" + culture + "/";
        }
        else
        {
            returnUrl = "~/" + culture + "/" + returnUrl.Substring(result + 1);
        }

        return LocalRedirect(returnUrl);
    }
}
