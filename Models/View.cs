﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace MyRazorPagesApp.Models;

public partial class View
{
    public int Id { get; set; }

    public int? PostId { get; set; }

    public int ViewCount { get; set; }

    public virtual Post? Post { get; set; }
}