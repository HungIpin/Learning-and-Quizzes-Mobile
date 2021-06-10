using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class Account
  {
    [Key]
    public int AccountId { get; set; }
    public string Username { get; set; }
    //Password hashcode round 4
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    public string Password { get; set; }
    public string Role { get; set; }
    public Boolean IsActive { get; set; }
    public Boolean Verification { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public ICollection<Course> Courses { get; set; }

    public ICollection<AccountinCourse> AccountinCourse { get; set; }
    public ICollection<AccountinLesson> AccountinLesson { get; set; }
  }
}
