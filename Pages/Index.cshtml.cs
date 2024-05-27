using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyRazorPagesApp.Models;
using MyRazorPagesApp.Data;
using System.Security;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MyRazorPagesApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? Topic { get; set; }
        
        private readonly IPostDto PostDto;
        
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, IPostDto postDt, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            PostDto = postDt;
            _userManager = userManager;
        }
        
        [BindProperty]
        public List<PostViewModel> Posts { get; set; } = default!;


        public async Task<IActionResult> OnGet()
        {

            Posts = await PostDto.GetPosts("Post");
            _logger.LogInformation("Post is not null");
            ViewData["PostId"] = new SelectList(Posts, "Id", "Id");

            return Page();
        }
        public async Task <string> GetUserNameById(string userId)
        {
            // var name = User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value??default;
            // Console.WriteLine("Username: >>>>>>>>>> " + name);
            var user = await _userManager.FindByIdAsync(userId);
            Console.WriteLine("Username: >>>>>>>>>> " + user.UserName);
            if (user != null)
            {
                return user.UserName;
            }
            return "Anonymous";
        }
        // public string GetUserNameFromClaims()
        // {
        //     var claimsIdentity = User.Identity as ClaimsIdentity;
        //     var userNameClaim = claimsIdentity?.FindFirst(ClaimTypes.Name);

        //     if (userNameClaim != null)
        //     {
        //         return userNameClaim.Value;
        //     }

        //     return null;
        // }

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
            //Post.ParentPostId = Post.Id; // Remove this line

            //_context.Posts.Add(Post);

            //await _context.SaveChangesAsync();

            // Now that the Post has been saved, its Id property has been set.
            // If you need to create a child post related to this post, you can use Post.Id as the ParentPostId.

            return RedirectToPage("./Index");
        }


    }

}