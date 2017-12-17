using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProjectLunar.API.Models
{
    public class LunarContext : DbContext
    {
        public LunarContext(DbContextOptions<LunarContext> options) : base(options) { }
        
        public DbSet<PrayerPlace> PrayingPlaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            modelBuilder.Entity<PrayerPlace>()
                .HasOne(a => a.User)
                .WithMany(s => s.PrayerPlaces)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>();

            modelBuilder.Entity<UserAction>()
                .HasOne(a => a.User)
                .WithMany(s => s.UserActions)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAction>()
                .HasOne(a => a.PrayerPlace)
                .WithMany(s => s.UserActions)
                .HasForeignKey(a => a.PrayerPlaceId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Photo>()
                .HasOne(a => a.PrayerPlace)
                .WithMany(s => s.Photos)
                .HasForeignKey(a => a.PrayerPlaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
