using MyRazorPagesApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MyRazorPagesApp.ViewModel
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        public int? PostId { get; set; }

        public string UserId { get; set; } = default!;

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(200, ErrorMessage = "Content cannot be longer than 200 characters.")]
        public string Content { get; set; } = default!;

        public DateTime? CommentTime { get; set; }

        public int? DownVote { get; set; }
        public int? UpVote { get; set; }
    }
}