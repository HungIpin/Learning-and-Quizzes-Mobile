using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class Lesson
  {
    [Key]
    public int LessonId { get; set; }
    public string LessonTitle { get; set; }
    public string LessonContent { get; set; }
    public int LessonNo { get; set; }
    public SubTopic SubTopic { get; set; }
    [Required]
    public int SubTopicId { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<AccountinLesson> UserinLesson { get; set; }
  }
}
