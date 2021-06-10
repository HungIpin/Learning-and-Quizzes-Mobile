using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class AccountinCourse
  {
    [Key]
    public int AccountinCourseID { get; set; }
    public Account Account { get; set; }
    [Required]
    public int AccountId { get; set; }
    public Course Course { get; set; }
    [Required]
    public int CourseId { get; set; }
    public int QuizCompleted { get; set; }
    public string InvoiceCode { get; set; }
    public DateTime CreatedDate { get; set; }
    public string PaymentMethod { get; set; }
    public Boolean IsBought { get; set; }
    public Boolean IsLiked { get; set; }
    public Boolean GetPayment { get; set; }
  }
}
