using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyRazorPagesApp.Models;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace MyRazorPagesApp.Pages.Posts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly MyRazorPagesApp.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        [BindProperty]
        public PostViewModel Post { get; set; } = default!;
        [BindProperty]
        public List<Community> Communities { get; set; } = default!;
        public CreateModel(MyRazorPagesApp.Data.ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            Communities = _context.Communities.ToList();
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(IFormFile? image)
        {
            Communities = _context.Communities.ToList();
            Post.CreatedDate = DateTime.Now;

            if (image == null || image.Length == 0)
            {
                   ModelState.AddModelError("image", "Please select a file.");
                   //return Page();
            }           

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (image != null)
            {
                if(image.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("image", "The file is too large.");
                    return Page();
                }
                var formats = new List<string> { ".jpg", ".png", ".gif", ".jpeg" };
                if (!formats.Contains(Path.GetExtension(image.FileName).ToLower()))
                {
                    ModelState.AddModelError("image", "The file must be a .jpg, .png, .gif, or .jpeg");
                    return Page();
                }
                //check for PostImages folder if not create it
                var path = Path.Combine(_environment.WebRootPath, "PostImages");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(_environment.WebRootPath, "PostImages", image.FileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                //Post.
                Post.PostPictureUrl = "/PostImages/" + image.FileName;
            }

            _context.Posts.Add(new Models.Post
            {
                CommunityId = Post.CommunityId,
                Content = Post.Content,
                CreatedDate = DateTime.Now,
                Downvotes = Post.Downvotes,
                ParentPostId = Post.ParentPostId,
                PostPictureUrl = Post.PostPictureUrl,
                PostType = "Post",
                Title = Post.Title,
                Upvotes = Post.Upvotes,
                UserId = User.FindFirst(x => x.Type.Contains("nameidentifier"))?.Value ?? "",
                
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");     
        }
    }
}
