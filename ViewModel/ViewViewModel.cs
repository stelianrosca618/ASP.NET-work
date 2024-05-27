using MyRazorPagesApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MyRazorPagesApp.ViewModel
{
    public class ViewViewModel
    {
    public int Id { get; set; }

    public int? PostId { get; set; }

    public int ViewCount { get; set; }

    public virtual Post? Post { get; set; }
    }
}