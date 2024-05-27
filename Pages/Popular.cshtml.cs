using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyRazorPagesApp.Models;

namespace MyRazorPagesApp.Pages
{
    [Authorize]
    public class Popular : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Topic { get; set; }

        private readonly MyRazorPagesApp.Data.ApplicationDbContext _context;

        private readonly ILogger<IndexModel> _logger;

        public Popular(ILogger<IndexModel> logger, MyRazorPagesApp.Data.ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Post> Posts { get; set; }


        public IActionResult OnGet()
        {
            //_context.Post is null
            if (_context.Posts == null)
            {
                _logger.LogInformation("Post is null");
            }
            else
            {
                _logger.LogInformation("Post is not null");
                ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
                //get a list of all the posts
                Posts = _context.Posts.ToList();

            }

            //add null check for line below

            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = new Post();
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        [BindProperty]
        public int PostId { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Post.CreatedDate = DateTime.Now;
            Post.UserId = User.Identity.Name;
            Post.PostType = "Question";
            // Post.Tags = new List<Tag> { new Tag { Name = "" }, new Tag { Name = "" } };
            Post.Content = Post.Content;
            // Post.InverseParentPost = new List<Post>();

            // Only set ParentPostId if this is a child post.
            // If this is a new parent post, leave ParentPostId null.
            // Post.ParentPostId = Post.Id; // Remove this line

            _context.Posts.Add(Post);

            await _context.SaveChangesAsync();

            // Now that the Post has been saved, its Id property has been set.
            // If you need to create a child post related to this post, you can use Post.Id as the ParentPostId.

            return RedirectToPage("./Index");
        }

    }
}
