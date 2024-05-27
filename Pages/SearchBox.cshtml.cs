using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPagesApp.Pages
{

    public class SearchBoxModel : PageModel
    {
        [BindProperty]
        public string Query { get; set; }="";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Handle the search query...
            return RedirectToPage("QuibbySearches", new { query = Query });
        }
    }
}