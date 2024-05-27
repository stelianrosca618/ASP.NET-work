using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using MyRazorPagesApp.Models;
using System.Security.Claims;

namespace MyRazorPagesApp.Pages
{
    public class Profile : PageModel
    {
        IAnswerDto AnswerDto;
        ICommentsDto CommentsDto;
        IPostDto PostDto;
        IViewsDto ViewsDto;

        public Profile(IAnswerDto answerDto, ICommentsDto commentsDto, IPostDto postDto, IViewsDto viewsDto)
        {
            AnswerDto = answerDto;
            CommentsDto = commentsDto;
            PostDto = postDto;
            ViewsDto = viewsDto;
        
        }

        [BindProperty] public List<AnswerViewModel> Answers { get; set; } = new();
        [BindProperty] public List<CommentViewModel> Comments { get; set; } = new();
        [BindProperty] public List<PostViewModel> Posts { get; set; } = new();
        [BindProperty] public List<ViewViewModel> Views { get; set; } = new();

        public async Task<IActionResult> OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Answers = await AnswerDto.GetAnswersByUser(userId);
           Comments = await CommentsDto.GetCommentsByUser(userId); //TODO: Implement GetCommentsByUser
          // Posts = await PostDto.GetPostsByUser(userId);// TODO: Implement GetPostsByUser
           // Views = await ViewsDto.GetView(); //TODO: Implement GetViewsByUser


            return Page();
        }
    }
}
