using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class Choice
  {
    [Key]
    public int ChoiceId { get; set; }
    public string Answer { get; set; }
    public Boolean IsCorrect { get; set; }
    public Byte[] AnswerImage { get; set; }
    public string AnswerImageLink { get; set; }
    public Quiz Quiz { get; set; }
    [Required]
    public int QuizId { get; set; }
    //public string QuestionCode { get; set; }
  }
}
