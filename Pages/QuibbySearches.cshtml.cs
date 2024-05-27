using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Models;

namespace MyRazorPagesApp.Pages
{
    public class QuibbySearchesModel : PageModel
    {
        private readonly MyRazorPagesApp.Data.ApplicationDbContext _context;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }= default!;

        public IQueryable<Post> Posts { get; set; } = default!;
        public IQueryable<Answer> Answers { get; set; } = default!;
        public IQueryable<Comment> Comments { get; set; } = default!;

        public IList<Post> PostsList { get; set; } = default!;
        public IList<Answer> AnswersList { get; set; } = default!;
        public IList<Comment> CommentsList { get; set; } = default!;

        public QuibbySearchesModel(MyRazorPagesApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            //search all the posts for the search string
            var posts = from p in _context.Posts
                        select p;
            var answers = from a in _context.Answers
                          select a;
            var comments = from c in _context.Comments
                           select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Title.Contains(searchString))
                             .Union(posts.Where(s => s.Content.Contains(searchString)));
                answers = answers.Where(s => s.Content.Contains(searchString))
                                 .Union(answers.Where(s => s.Content.Contains(searchString)));
                comments = comments.Where(s => s.Content.Contains(searchString))
                                   .Union(comments.Where(s => s.Content.Contains(searchString)));
            }
            PostsList = await posts.ToListAsync();
            AnswersList = await answers.ToListAsync();
            CommentsList = await comments.ToListAsync();
            foreach (var post in PostsList)
            {
                Console.WriteLine(post.Title);
                Console.WriteLine(post.Content);

            }
            return Page();

        }
        public async Task<IActionResult> ProcessSearch()
        {
            foreach(var post in PostsList)
            {
                Console.WriteLine(post.Title);
                Console.WriteLine(post.Content);
                
            }
            return Page();
        }
    }

}
