#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyRazorPagesApp.Models;

public class PostTopic
{

    public int Id { get; set; }
    public int PostID { get; set; }
    public Post Post { get; set; }
    public int TopicID { get; set; }
    public Topic Topic { get; set; }
}