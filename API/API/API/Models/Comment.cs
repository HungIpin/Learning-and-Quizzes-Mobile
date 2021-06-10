using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class Comment
  {
    [Key]
    public int CommentId { get; set; }
    public string CommentContent { get; set; }
    public float Rating { get; set; }
    public DateTime DatePost { get; set; }
    public string Type { get; set; }
    public Boolean IsLiked { get; set; }
    public int LikeCount { get; set; }
    public ICollection<SubComment> SubComments { get; set; }
    public User User { get; set; }
    [Required]
    public int UserId { get; set; }
    public Course Course { get; set; }
    public int? CourseId { get; set; }

    public string? LessonId { get; set; }
  }
}
