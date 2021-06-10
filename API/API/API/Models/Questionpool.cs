using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class Questionpool
  {
    [Key]
    public int QuestionpoolId { get; set; }
    public string QuestionpoolName { get; set; }
    //public string Question { get; set; }
    //public int Point { get; set; }
    //public int Time { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastEdited { get; set; }
    public string Hastag { get; set; }
    public Byte[] QuestionpoolThumbnailImage { get; set; }
    public string QuestionpoolThumbnailImageURL { get; set; }
    public Boolean IsActive { get; set; }
    public string AccountId { get; set; }
    public Course Course { get; set; }
    [Required]
    public int CourseId { get; set; }
    public ICollection<Quiz> Quizs { get; set; }
  }
}
