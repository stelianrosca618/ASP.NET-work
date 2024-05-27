CREATE TABLE AspNetUsers (
    Id NVARCHAR(450) PRIMARY KEY,
    UserName NVARCHAR(256),
    NormalizedUserName NVARCHAR(256),
    Email NVARCHAR(256),
    NormalizedEmail NVARCHAR(256),
    EmailConfirmed BIT,
    PasswordHash NVARCHAR(MAX),
    SecurityStamp NVARCHAR(MAX),
    ConcurrencyStamp NVARCHAR(MAX),
    PhoneNumber NVARCHAR(MAX),
    PhoneNumberConfirmed BIT,
    TwoFactorEnabled BIT,
    LockoutEnd DATETIMEOFFSET,
    LockoutEnabled BIT,
    AccessFailedCount INT
);

CREATE TABLE Posts (
    PostID INT PRIMARY KEY,
    UserID NVARCHAR(450),
    Title VARCHAR(255),
    Content TEXT,
    PostType NVARCHAR(255),
    ParentPostID INT DEFAULT NULL,
    CreatedDate datetime(6) DEFAULT NULL,
    IsNSFW BOOLEAN,
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);

-- CREATE TABLE `posts` (
--   `Id` int NOT NULL AUTO_INCREMENT,
--   `Title` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
--   `Content` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
--   `PostType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
--   `ParentPostId` int DEFAULT NULL,
--   `UserId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
--   `CreatedDate` datetime(6) DEFAULT NULL,
--   `PostPictureUrl` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
--   PRIMARY KEY (`Id`),
--   KEY `IX_Post_ParentPostId` (`ParentPostId`),
--   CONSTRAINT `FK_Post_Post_ParentPostId` FOREIGN KEY (`ParentPostId`) REFERENCES `post` (`Id`)
-- ) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


CREATE TABLE Comments (
    CommentID INT PRIMARY KEY,
    PostID INT,
    UserID NVARCHAR(450),
    Content TEXT,
    CommentTime DATETIME,
    FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);

CREATE TABLE Votes (
    VoteID INT PRIMARY KEY,
    PostID INT,
    UserID NVARCHAR(450),
    IsUpvote BOOLEAN,
    Isdownvote BOOLEAN,
    FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);

CREATE TABLE Reports (
    ReportID INT PRIMARY KEY,
    PostID INT,
    UserID NVARCHAR(450),
    ReportTime DATETIME,
    FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);

CREATE TABLE ModerationActions (
    ActionID INT PRIMARY KEY,
    ModeratorID NVARCHAR(450),
    PostID INT,
    Action NVARCHAR(255),
    ActionTime DATETIME,
    FOREIGN KEY (ModeratorID) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (PostID) REFERENCES Posts(PostID)
);


CREATE TABLE Profiles (
    UserID NVARCHAR(450) PRIMARY KEY,
    Bio TEXT,
    Location NVARCHAR(256),
    ProfilePicture NVARCHAR(MAX),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);

CREATE TABLE Topics (
    TopicID INT PRIMARY KEY,
    TopicName NVARCHAR(256)
);

CREATE TABLE PostTopics (
    PostID INT,
    TopicID INT,
    PRIMARY KEY (PostID, TopicID),
    FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    FOREIGN KEY (TopicID) REFERENCES Topics(CategoryID)
);

CREATE TABLE Followers (
    FollowerID NVARCHAR(450),
    FollowingID NVARCHAR(450),
    PRIMARY KEY (FollowerID, FollowingID),
    FOREIGN KEY (FollowerID) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (FollowingID) REFERENCES AspNetUsers(Id)
);

CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY,
    UserID NVARCHAR(450),
    NotificationText NVARCHAR(MAX),
    NotificationTime DATETIME,
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);

CREATE TABLE Answers (
    AnswerID INT PRIMARY KEY,
    PostID INT,
    UserID NVARCHAR(450),
    Content TEXT,
    AnswerTime DATETIME,
    FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);

CREATE TABLE PostViews (
    PostID INT,
    UserID NVARCHAR(450),
    ViewTime DATETIME,
    PRIMARY KEY (PostID, UserID),
    FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);

CREATE TABLE UserActivity (
    ActivityID INT PRIMARY KEY,
    UserID NVARCHAR(450),
    Activity NVARCHAR(MAX),
    ActivityTime DATETIME,
    FOREIGN KEY (UserID) REFERENCES AspNetUsers(Id)
);