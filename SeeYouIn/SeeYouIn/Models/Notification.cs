using SeeYouIn.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SeeYouIn.Models
{
  public class Notification
  {
    [Key]
    public int ID { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime UntilDate { get; set; }
    public NotificationFrequency FrequencyToSend { get; set; }
    public int ReminderID { get; set; }

    public Notification(string title, string body, DateTime untilDate, NotificationFrequency frequencyToSend)
    {
      Title = title;
      Body = body;
      UntilDate = untilDate;
      FrequencyToSend = frequencyToSend;
    }

    public Notification(string title, string body, DateTime untilDate, NotificationFrequency frequencyToSend,int reminderID)
    {
      Title = title;
      Body = body;
      UntilDate = untilDate;
      FrequencyToSend = frequencyToSend;
      ReminderID = reminderID;
    }

    public Notification(Reminder reminder, NotificationFrequency notificationFrequency = NotificationFrequency.DAILY)
    {
      Title = reminder.Title;
      Body = reminder.Body;
      UntilDate = reminder.ETA;
      FrequencyToSend = notificationFrequency;
      ReminderID = reminder.ID;
    }
  }
}
