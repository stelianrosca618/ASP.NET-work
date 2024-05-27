using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Dtos;
using MyRazorPagesApp.Models;
using System.Security.Claims;

namespace MyRazorPagesApp.Pages.Question
{
    public class CreateModel : PageModel
    {
        IQuestionDto questionDto;
        public CreateModel(IQuestionDto questionDto)
        {
            this.questionDto = questionDto;
        }

        [BindProperty]
        public QuestionViewModel Question { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int PostId { get; set; }
        public void OnGet(int id)
        {
            PostId = id;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Question.UserId = User.FindFirst(x => x.Type.Contains("nameidentifier"))?.Value ?? "";

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await questionDto.CreateQuestion(Question);

            return RedirectToPage("../Index");
        }
    }
}
