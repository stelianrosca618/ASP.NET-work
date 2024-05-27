// Controllers/VotesController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;  // Replace with your actual DbContext namespace
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class VotesController : ControllerBase
{
    private readonly ApplicationDbContext _context;  // Replace with your actual DbContext

    public VotesController(ApplicationDbContext context)  // Replace with your actual DbContext
    {
        _context = context;
    }
    public long upvotes { get; set; } 
    public long downvotes { get; set; }
    [HttpGet("upvotes/{answerId}")]
    public async Task<ActionResult<int>> GetUpvotes(int answerId)
    {
        if (answerId == 0)
        {
            return BadRequest();
        }
        else
        {
        upvotes = await _context.Votes.AsNoTracking()
            .Where(v => v.AnswerId == answerId)
            .SumAsync(v => v.Upvote);

        return Ok(upvotes);
        }
    }


    [HttpGet("downvotes/{answerId}")]
    public async Task<ActionResult<int>> GetDownvotes(int answerId)
    {
        if (answerId == 0)
        {
            return BadRequest();
        }
        else
        {
            downvotes = await _context.Votes.AsNoTracking()
                .Where(v => v.AnswerId == answerId)
                .SumAsync(v => v.Downvote);

            return Ok(downvotes);
        }
    }
}