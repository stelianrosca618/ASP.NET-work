using System.ComponentModel.DataAnnotations;

namespace MyRazorPagesApp.Models
{

    public class Community
    {
        public int CommunityID { get; set; }
        [Required]
        public string CommunityName { get; set; }
        public string Description { get; set; }
        public string Topic { get; set; }
        public bool IsAdultContent { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }

}
// public class CommunityMember
// {
//     public int CommunityID { get; set; }
//     public string UserID { get; set; }

//     public Community Community { get; set; }
//     // Assuming you have a User class
//     public User User { get; set; }
// }

// public class CommunityPost
// {
//     public int CommunityID { get; set; }
//     public int PostID { get; set; }

//     public Community Community { get; set; }
//     // Assuming you have a Post class
//     public Post Post { get; set; }
// }










// a `Communities` table can be added to group related topics together.
// Users can join communities and posts can be associated with communities. Here's how you might define the `Communities` table and the associated junction tables:

// ```sql
// CREATE TABLE Communities (
//     CommunityID INT PRIMARY KEY,
//     CommunityName NVARCHAR(256),
//     Description TEXT
// );

// CREATE TABLE CommunityMembers (
//     CommunityID INT,
//     UserID NVARCHAR(450),
//     PRIMARY KEY (CommunityID, UserID),
//     FOREIGN KEY (CommunityID) REFERENCES Communities(CommunityID),
//     FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
// );

// CREATE TABLE CommunityPosts (
//     CommunityID INT,
//     PostID INT,
//     PRIMARY KEY (CommunityID, PostID),
//     FOREIGN KEY (CommunityID) REFERENCES Communities(CommunityID),
//     FOREIGN KEY (PostID) REFERENCES Posts(PostID)
// );
// ```

// In this design, the `Communities` table stores each community's ID and name. The `CommunityMembers` table tracks which users are members of which communities, and the `CommunityPosts` 
//table tracks which posts are associated with which communities.