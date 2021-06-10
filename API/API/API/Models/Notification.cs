using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
  public class Notification
  {
    [Key]
    public int NotificationId { get; set; }
    public string MessageTitle { get; set; }
    [StringLength(200, ErrorMessage = "Invalid message.", MinimumLength = 10)]
    public string Message { get; set; }
    public string Type { get; set; }
    public string SendTo { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
