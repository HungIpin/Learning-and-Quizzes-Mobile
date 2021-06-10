using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastLogOnDate { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public Byte[] AvatarPath { get; set; }
    public float Balance { get; set; }
    //public int AccountId { get; set; }
    public Account Account{ get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<SubComment> SubComments { get; set; }
  }
}
