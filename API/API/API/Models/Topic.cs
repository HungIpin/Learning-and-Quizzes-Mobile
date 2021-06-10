using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class Topic
  {
    [Key]
    public int TopicId { get; set; }
    public string TopicTitle { get; set; }
    public int SessionNumber { get; set; }
    public DateTime LastUpdate{ get; set; }
    public Boolean IsLocked { get; set; }
    public Course Course { get; set; }
    [Required]
    public int CourseId { get; set; }
    public ICollection<SubTopic> SubTopics { get; set; }
  }
}
