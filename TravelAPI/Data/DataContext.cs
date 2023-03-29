using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Xml.Linq;
using TravelAPI.Models;

namespace TravelAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-BQCVFF2\\JPMARINT; Database=TravelDb; Trusted_Connection=true; TrustServerCertificate=true;");
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
