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
    public class CommentModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
