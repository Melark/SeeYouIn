using System;

namespace SeeYouIn.Models
{
  public class MainPageMenuItem
  {
    public MainPageMenuItem()
    {
    }
    public int Id { get; set; }
    public string Title { get; set; }

    public Type TargetType { get; set; }

    public bool Enabled { get; set; } = true;
  }
}
