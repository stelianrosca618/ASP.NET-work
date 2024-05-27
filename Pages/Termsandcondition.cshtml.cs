using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyRazorPagesApp.Pages;

public class TermsandconditionModel : PageModel
{
    private readonly ILogger<TermsandconditionModel> _logger;

    public TermsandconditionModel(ILogger<TermsandconditionModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
