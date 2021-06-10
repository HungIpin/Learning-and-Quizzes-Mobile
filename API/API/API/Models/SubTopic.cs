using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class SubTopic
  {
    [Key]
    public int SubTopicId { get; set; }
    public string SubTopicTitle { get; set; }
    public int SubTopicNumber { get; set; }
    public Topic Topic { get; set; }
    [Required]
    public int TopicId { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
  }
}
