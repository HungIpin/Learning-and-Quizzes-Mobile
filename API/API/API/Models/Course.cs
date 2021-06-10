using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class Course
  {
    [Key]
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public float Rating { get; set; }
    public double NumberOfVoters { get; set; }
    public double NumberOfParticipants { get; set; }
    public float Price { get; set; }
    public DateTime StartDate { get; set; }
    public string CourseDuration { get; set; }
    public string Description { get; set; }
    public byte[] ThumbnailImage { get; set; }
    public string Hastag { get; set; }
    public string Level { get; set; }
    public DateTime LastUpdate { get; set; }
    public int LessonNumber { get; set; }
    public Boolean IsActive { get; set; }
    public int ViewCount { get; set; }
    public Account Account { get; set; }
    [Required]
    public int AccountId { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Questionpool> Questionpools { get; set; }
    public ICollection<AccountinCourse> UserinCourse { get; set; }
    public ICollection<Topic> Topics { get; set; }
  }
}
