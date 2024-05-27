// Controllers/VotesController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;  // Replace with your actual DbContext namespace
using MyRazorPagesApp.Models;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PostsVotesController : ControllerBase
{
    private readonly ApplicationDbContext _context;  // Replace with your actual DbContext

    public PostsVotesController(ApplicationDbContext context)  // Replace with your actual DbContext
    {
        _context = context;
    }
    public long Postsupvotes { get; set; } 
    public long Postsdownvotes { get; set; }
    [HttpGet("Postsupvotes/{PostId}")]
    public async Task<ActionResult<int>> GetUpvotes(int postId)
    {
        if (postId == 0)
        {
            return BadRequest();
        }
        else
        {
        Postsupvotes = await _context.Votes.AsNoTracking()
            .Where(v => v.PostId == postId)
            .SumAsync(v => v.Upvote);

        return Ok(Postsupvotes);
        }
    }

    [HttpGet("Postsdownvotes/{answerId}")]
    public async Task<ActionResult<int>> GetDownvotes(int postId)
    {
        if (postId == 0)
        {
            return BadRequest();
        }
        else
        {
            Postsdownvotes = await _context.Votes.AsNoTracking()
                .Where(v => v.PostId == postId)
                .SumAsync(v => v.Downvote);

            return Ok(Postsdownvotes);
        }
    }
}