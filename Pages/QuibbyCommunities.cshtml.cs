using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorPagesApp.Models;

namespace MyRazorPagesApp.Pages
{
    [Authorize]
    public class QuibbyCommunities : PageModel
    {
        private readonly MyRazorPagesApp.Data.ApplicationDbContext _context;
        private readonly ILogger<QuibbyTopicModel> _logger;
        
        [BindProperty] public Community Community { get; set; }
        [BindProperty] public List<Community> Communities { get; set; }

        public QuibbyCommunities(ILogger<QuibbyTopicModel> logger, Data.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Communities = _context.Communities.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Communities.Add(Community);
            await _context.SaveChangesAsync();

            Communities = _context.Communities.ToList();
            Community = new();
            return Page();
        }
    }
}
