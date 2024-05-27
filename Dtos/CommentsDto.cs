using Microsoft.EntityFrameworkCore;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.Models;

namespace MyRazorPagesApp.Dtos
{
    public interface ICommentsDto
    {
        Task<int> CreateComment(Comment comment);
        Task<bool> DeleteComments(int id);
        Task<List<CommentViewModel>> GetComments();
        Task<int> UpdateComment(Comment update);
        Task<List<CommentViewModel>> GetCommentsByUser(string userId);
        Task<List<CommentViewModel>> GetCommentsByPostId(int postId);
        Task<bool> CheckCommentExists(int postId);
    }

    public class CommentsDto : ICommentsDto
    {
        protected readonly ApplicationDbContext _context;
        public CommentsDto(ApplicationDbContext context)
        {
            this._context = context;
        }
        //implement CheckCommentExists
        public async Task<bool> CheckCommentExists(int postId)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(x => x.PostId == postId);
            if (existingComment != null)
            {
                return true;
            }
            return false;
        }
        public async Task<int> CreateComment(Comment comment)
        {
            try
            {
                if (comment != null && comment.PostId != 0)
                {
                    var existingTopicPost = await _context.Posts.FirstOrDefaultAsync(x => x.Id == comment.PostId);
                    if (existingTopicPost != null)
                    {
                        var entity = _context.Comments.Add(
                            new Comment
                            {
                                CommentTime = DateTime.Now,
                                Content = comment.Content,
                                PostId = comment.PostId,
                                UserId = comment.UserId,
                                DownVote = comment.DownVote,
                                UpVote = comment.UpVote

                            });
                        await _context.SaveChangesAsync();

                        return entity.Entity.CommentId;
                    }
                }
            }
            catch { }

            return 0;
        }

        public async Task<List<CommentViewModel>> GetComments()
        {
            var Comments = new List<CommentViewModel>();
            try
            {
                var existingPost = await _context.Comments.ToListAsync();
                Comments = existingPost.Select(x => new CommentViewModel
                {
                    CommentId = x.CommentId,
                    Content = x.Content,
                    CommentTime = x.CommentTime,
                    DownVote = x.DownVote,
                    UpVote = x.UpVote,
                    PostId = x.PostId,
                    UserId = x.UserId
                }).ToList();
                return Comments;
            }
            catch
            {
                return new List<CommentViewModel>();
            }
        }
        public async Task<List<CommentViewModel>> GetCommentsByUser(string userId)
        {
            var Comments = new List<CommentViewModel>();
            try
            {
                var existingPost = await _context.Comments.Where(x => x.UserId == userId).ToListAsync();
                var postIds = existingPost.Select(x => x.PostId).ToList();
                var existingTopicPost = await _context.Posts.Where(x => postIds.Contains(x.Id)).ToListAsync();

                var commentsperuser = from c in existingPost
                             join p in existingTopicPost on c.PostId equals p.Id
                             select new CommentViewModel
                             {
                                 CommentId = c.CommentId,
                                 Content = c.Content,
                                 CommentTime = c.CommentTime,
                                 DownVote = c.DownVote,
                                 UpVote = c.UpVote,
                                 PostId = c.PostId,                                 
                                 UserId = c.UserId
                             };
                Comments = commentsperuser.ToList();
                
                return Comments;
            }
            catch
            {
                return new List<CommentViewModel>();
            }
        }

        public async Task<List<CommentViewModel>> GetCommentsByPostId(int postId)
        {
            var Comments = new List<CommentViewModel>();
            try
            {
                var existingPost = await _context.Comments.Where(x => x.PostId == postId).ToListAsync();
                Comments = existingPost.Select(x => new CommentViewModel
                {
                    CommentId = x.CommentId,
                    Content = x.Content,
                    CommentTime = x.CommentTime,
                    DownVote = x.DownVote,
                    UpVote = x.UpVote,
                    PostId = x.PostId,
                    UserId = x.UserId
                }).ToList();
                return Comments;
            }
            catch
            {
                return new List<CommentViewModel>();
            }
        }
        public async Task<int> UpdateComment(Comment update)
        {
            try
            {
                var existing = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == update.CommentId);

                if (existing != null)
                {
                    existing.Content = update.Content;
                    existing.CommentTime = DateTime.Now;
                    existing.UpVote = update.UpVote;
                    existing.DownVote = update.DownVote;

                };
                await _context.SaveChangesAsync();

                return existing.CommentId;
            }
            catch (Exception ex) { }

            return 0;
        }
        // public async Task<List<Comment>> GetComments()
        // {
        //     var Answers = new List<Comment>();
        //     try
        //     {
        //         var existingPost = await _context.Comments.ToListAsync();
        //         Answers = existingPost;
        //         return Answers;
        //     }
        //     catch { }
        //     return null;
        // }
        
        public async Task<bool> DeleteComments(int id)
        {

            try
            {
                var TopicToDel = await _context.Comments.SingleOrDefaultAsync(x => x.CommentId == id);
                if (TopicToDel != null)
                {
                    _context.Comments.Remove(TopicToDel);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
