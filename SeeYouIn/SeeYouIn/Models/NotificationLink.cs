using System.ComponentModel.DataAnnotations;

namespace SeeYouIn.Models
{
  public class NotificationLink
  {
    [Key]
    public int ID { get; set; } // Also functions as the notification ID

    public int ReminderID { get; set; }
    public int NotificationID { get; set; }
  }
}
