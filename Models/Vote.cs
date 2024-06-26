﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace MyRazorPagesApp.Models;

public partial class Vote
{
    public int VoteId { get; set; }

    public string? UserId { get; set; }

    public int? PostId { get; set; }
    public virtual Post Post { get; set; }
    public int? CommentId { get; set; }
    public virtual Comment Comment { get; set; }

    public int? AnswerId { get; set; }
    public virtual Answer Answer { get; set; }

    public long Upvote { get; set; }

    public long Downvote { get; set; }


}