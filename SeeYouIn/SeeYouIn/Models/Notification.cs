using SeeYouIn.Enums;
using System;

namespace SeeYouIn.Models
{
  public class Notification
  {
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime UntilDate { get; set; }
    public NotificationFrequency FrequencyToSend { get; set; }

    public Notification(string title, string body, DateTime untilDate, NotificationFrequency frequencyToSend)
    {
      Title = title;
      Body = body;
      UntilDate = untilDate;
      FrequencyToSend = frequencyToSend;
    }
  }
}
