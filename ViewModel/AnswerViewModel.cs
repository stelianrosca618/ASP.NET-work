using MyRazorPagesApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MyRazorPagesApp.ViewModel
{
    public class AnswerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(1000, ErrorMessage = "Content cannot be longer than 1000 characters.")]
        public string Content { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int PostId { get; set; }
        public int PostTopicId { get; set; }


        public int? DownVote { get; set; }
        public int? UpVote { get; set; }
        public string? Question { get; set; }
    }
}
