using System;
using System.ComponentModel.DataAnnotations;

namespace SeeYouIn.Models
{
    public class Reminder
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime ETA { get; set; }
  }
}
