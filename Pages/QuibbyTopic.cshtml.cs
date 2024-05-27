using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPagesApp.Pages
{
    public class QuibbyTopicModel : PageModel
    {
        private readonly ILogger<QuibbyTopicModel> _logger;

        public QuibbyTopicModel(ILogger<QuibbyTopicModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
