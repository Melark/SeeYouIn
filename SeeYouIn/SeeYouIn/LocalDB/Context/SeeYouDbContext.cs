using Microsoft.EntityFrameworkCore;
using SeeYouIn.Models;
using System;
using System.IO;
using Xamarin.Forms;

namespace SeeYouIn.LocalDB.Context
{
    public class SeeYouDbContext : DbContext
    {
        public DbSet<Reminder> Reminders { get; set; }

        private const string databaseName = "SeeYouIn.db";

        public SeeYouDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String databasePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName); ;
                    break;
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}
