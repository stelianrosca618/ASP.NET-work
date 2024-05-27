using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Models;
using MyRazorPagesApp.Data;

namespace MyRazorPagesApp.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly MyRazorPagesApp.Data.ApplicationDbContext _context;

        public IndexModel(MyRazorPagesApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Post = await _context.Posts.ToListAsync();
        }
    }
}
