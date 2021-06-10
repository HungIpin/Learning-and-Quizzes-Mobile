using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class ExamQuiz
  {
    [Key]
    public int ExamQuizId { get; set; }
    public string ExamQuizName { get; set; }
    public string ExamQuestion { get; set; }
    public string ExamIsCorrect { get; set; }
    public string ExamOption1 { get; set; }
    public string ExamOption2 { get; set; }
    public string ExamOption3 { get; set; }
    public string ExamOption4 { get; set; }
    public string ExamOption5 { get; set; }
    public string ExamQuestionImageURL { get; set; }
    public string ExamOptionImageURL1 { get; set; }
    public string ExamOptionImageURL2 { get; set; }
    public string ExamOptionImageURL3 { get; set; }
    public string ExamOptionImageURL4 { get; set; }
    public string ExamOptionImageURL5 { get; set; }
    public Byte[] ExamThumbnailImage { get; set; }
    public Byte[] ExamQuestionImage { get; set; }
    public Byte[] ExamOptionImage1 { get; set; }
    public Byte[] ExamOptionImage2 { get; set; }
    public Byte[] ExamOptionImage3 { get; set; }
    public Byte[] ExamOptionImage4 { get; set; }
    public Byte[] ExamOptionImage5 { get; set; }
    public string ExamTagTopic { get; set; }
    public string ExamQuizCode { get; set; }
    public string CourseId { get; set; }
    public int ExamTime { get; set; } 
    public string QuizId { get; set; }
    public Boolean IsBlocked { get; set; }
  }
}
