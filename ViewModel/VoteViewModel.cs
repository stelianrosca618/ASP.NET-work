namespace MyRazorPagesApp.ViewModel
{
    public class VoteViewModel
    {
        public ContentEntity Entity { get; set; }
        public int Id { get; set; }
        public int VoteId { get; set; }

        public string? UserId { get; set; }

        public int? PostId { get; set; }
        public int? CommentId { get; set; }

        public int? AnswerId { get; set; }

        public long Upvote { get; set; }

        public long Downvote { get; set; }
    }
}
