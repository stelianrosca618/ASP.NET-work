using MyRazorPagesApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MyRazorPagesApp.ViewModel
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(300, ErrorMessage = "Content cannot be longer than 300 characters.")]
        public string Content { get; set; } = "";
        public string? PostType { get; set; }
        public int? ParentPostId { get; set; }
        public string? UserId { get; set; }
        public int? CommunityId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? PostPictureUrl { get; set; }
        public int? Upvotes { get; set; }
        public int? Downvotes { get; set; }
        public string? Username { get; set; }
    }
}
