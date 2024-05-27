using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MyRazorPagesApp.Data;
using MyRazorPagesApp.Models;
using MyRazorPagesApp.ViewModel;

namespace MyRazorPagesApp.Dtos
{
    public interface IPostDto
    {
        Task<int> CreatePost(PostViewModel newPost);
        Task<bool> DeletePost(int id);
        Task<PostViewModel> GetPostById(int id);
        Task<List<PostViewModel>> GetPosts(string postType);
        Task<int> UpdatePost(PostViewModel newPost);
    }

    public class PostDto : IPostDto
    {
        protected ApplicationDbContext _context;

        //private readonly UserManager<ApplicationUser> _userManager;
        public PostDto(ApplicationDbContext context)
        {
            this._context = context;
            //_userManager = userManager;
        }

        //#region Properties
        //public int Id { get; set; }

        //public string? Title { get; set; }

        //public string Content { get; set; }
        //public string? PostType { get; set; }
        //public int? ParentPostId { get; set; }
        //public string? UserId { get; set; }
        //public int CommunityId { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string? PostPictureUrl { get; set; }

        //public int? Upvotes { get; set; }
        //public int? Downvotes { get; set; }
        //#endregion

        //#region Methods

        public async Task<int> CreatePost(PostViewModel newPost)
        {
            try
            {
                if (newPost != null)
                {
                    var entity = _context.Posts.Add(new Post
                    {
                        CommunityId = newPost.CommunityId.GetValueOrDefault(),
                        Content = newPost.Content,
                        CreatedDate = DateTime.Now,
                        Downvotes = newPost.Downvotes,
                        ParentPostId = newPost.ParentPostId,
                        PostPictureUrl = newPost.PostPictureUrl,
                        PostType = newPost.PostType,
                        Title = newPost.Title,
                        Upvotes = newPost.Upvotes,
                        UserId = newPost.UserId,
                    });
                    await _context.SaveChangesAsync();

                    return entity.Entity.Id;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return 0;
        }
        public async Task<int> UpdatePost(PostViewModel newPost)
        {
            try
            {
                var existing = await _context.Posts.FirstOrDefaultAsync(x => x.Id == newPost.Id);

                if (existing != null)
                {
                    existing.CommunityId = newPost.CommunityId.GetValueOrDefault();
                    existing.Content = newPost.Content;
                    existing.Downvotes = newPost.Downvotes;
                    existing.ParentPostId = newPost.ParentPostId;
                    existing.PostPictureUrl = newPost.PostPictureUrl;
                    existing.PostType = newPost.PostType;
                    existing.Title = newPost.Title;
                    existing.Upvotes = newPost.Upvotes;
                    existing.UserId = newPost.UserId;
                };
                await _context.SaveChangesAsync();

                return existing.Id;
            }
            catch (Exception ex) { }
            return 0;
        }
        public async Task<List<PostViewModel>> GetPosts(string postType)
        {
            var Posts = new List<PostViewModel>();
            try
            {
                //var users = await _userManager.Users.ToListAsync();

                var existingPost = await _context.Posts.Where(x => x.PostType == postType).ToListAsync();

                Posts = existingPost.Select(x => new PostViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    //Username = users.FirstOrDefault(u => u.Id == x.UserId)?.UserName,
                    CommunityId = x.CommunityId,
                    Content = x.Content,
                    CreatedDate = x.CreatedDate,
                    Downvotes = x.Downvotes,
                    ParentPostId = x.ParentPostId,
                    PostPictureUrl = x.PostPictureUrl,
                    PostType = postType,
                    Title = x.Title,
                    Upvotes = x.Upvotes
                }).ToList();
            }
            catch { }

            return Posts;
        }
        public async Task<PostViewModel> GetPostById(int id)
        {
            var Post = new PostViewModel();
            try
            {
                var postby = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);

                Post = new PostViewModel
                {
                    CommunityId = postby.CommunityId,
                    Content = postby.Content,
                    CreatedDate = postby.CreatedDate,
                    Downvotes = postby.Downvotes,
                    Id = postby.Id,
                    ParentPostId = postby.ParentPostId,
                    Upvotes = postby.Upvotes,
                    PostPictureUrl = postby.PostPictureUrl,
                    PostType = postby.PostType,
                    Title = postby.Title,
                    UserId = postby.UserId
                };

                return Post;

            }
            catch { }
            return null;
        }
        public async Task<bool> DeletePost(int id)
        {
            var Post = new Post();
            try
            {
                var PosttoDel = await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);
                if (PosttoDel != null)
                {
                    _context.Posts.Remove(PosttoDel);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch { }

            return false;
        }
        //#endregion
    }
}

