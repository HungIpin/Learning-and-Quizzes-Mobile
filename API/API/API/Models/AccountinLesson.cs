using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class AccountinLesson
  {
    [Key]
    public int AccountinLessonID { get; set; }
    public string AccountId { get; set; }
    public string ExamQuizCode { get; set; }
    public string QuizName { get; set; }
    public Boolean IsCompleted { get; set; }
    public string Result { get; set; }
    public DateTime LastTaken { get; set; }
  }
}
