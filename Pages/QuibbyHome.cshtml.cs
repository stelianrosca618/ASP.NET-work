using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyRazorPagesApp.Models;
using MyRazorPagesApp.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MyRazorPagesApp.Pages
{
    [Authorize]
    public class QuibbyHomeModel : PageModel
    {
        private readonly MyRazorPagesApp.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<IndexModel> _logger;

        public QuibbyHomeModel(UserManager<IdentityUser> userManager, ILogger<IndexModel> logger,MyRazorPagesApp.Data.ApplicationDbContext context)
            {
                _context = context;
                _logger = logger;
                _userManager = userManager;
            }
        [BindProperty]
        public int PostId { get; set; }

        [BindProperty]
        public IList<Post> Questions { get; set; }
        [BindProperty]
        public IList<Answer> Answers { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public Answer Answer { get; set; }
        public List<Vote> Votes { get; set; }


        //[BindProperty(SupportsGet = true)]
        //public string answer { get; set; } = "Some Value";

        public async Task<IActionResult> OnGet(int id)
        {
            PostId = id;
            //var communityIds = await _context.Communities
            //    .Where(x => x.Topic == Topic)
            //    .Select(c => c.CommunityID)
            //    .ToListAsync();

            var posts = await _context.Posts
                .Where(p => p.ParentPostId == id)
                .ToListAsync();

            var answers = _context.Answers
                .AsEnumerable()
                .Where(a => posts.Any(p => p.Id == a.PostId))
                .ToList();

            Questions = posts;
            Answers = answers;

            Votes = _context.Votes.ToList();
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            PostId = id;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //get username from usermanager
            var user = _userManager.FindByIdAsync(userId).Result;
            var username = user.UserName;
            var newAnswer = new Answer
            {
                Content = Answer.Content,
                CreatedDate = DateTime.Now,
                //get PostId of the question
                PostId = Answer.PostId,
                UserId = userId
            };

            _context.Answers.Add(newAnswer);
            _context.SaveChanges();
            
            return RedirectToPage("QuibbyHome", new { id = PostId });
        }

   public async Task<IActionResult> OnPostUpvoteAnswerAsync([FromBody] int id)

    {
        var answer = await _context.Answers.FindAsync(id);
        if (answer == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Check if a vote by this user already exists for this answer
        var existingVote = await _context.Votes.FirstOrDefaultAsync(v => v.UserId == userId && v.AnswerId == id);

        if (existingVote != null)
        {
            // If a vote already exists, increment the Upvote property
            existingVote.Upvote++;
            _context.Entry(existingVote).State = EntityState.Modified;
        }
        else
        {
            // If no vote exists, create a new one
            var vote = new Vote
            {
                UserId = userId,
                AnswerId = id,
                Upvote = 1
            };
            _context.Votes.Add(vote);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AnswerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // Get the total upvotes for the answer
        var totalUpvotes = await _context.Votes.Where(v => v.AnswerId == id).SumAsync(v => v.Upvote);

        return new JsonResult(new { success = true, upvotes = totalUpvotes });
    }

    private bool AnswerExists(int id)
    {
        return _context.Answers.Any(e => e.Id == id);
    }
        public async Task<IActionResult> OnPostGetUpvoteCountAsync(int id)
        {
            var upvotes = await _context.Votes
                .Where(v => v.AnswerId == id)
                .SumAsync(v => v.Upvote);

            return new JsonResult(new { success = true, upvotes = upvotes });
        }
    }
   
}