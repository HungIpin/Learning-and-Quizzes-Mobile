using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class SubComment
  {
    [Key]
    public int SubCommentId { get; set; }
    public string SubCommentContent { get; set; }
    public DateTime SubDatePost { get; set; }
    public Boolean IsLiked { get; set; }
    public int LikeCount { get; set; }
    public Comment ParentComment { get; set; }
    public int? ParentCommentId { get; set; }
    public User User { get; set; }
    [Required]
    public int UserId { get; set; }
  }
}
