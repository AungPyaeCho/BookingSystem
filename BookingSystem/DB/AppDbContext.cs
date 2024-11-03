using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace BookingSystem.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<PackageModel> Packages { get; set; }
        public DbSet<UserPackageModel> UserPackages { get; set; }
        public DbSet<ClassModel> Classes { get; set; }
        public DbSet<StatusModel> Status { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<WaitlistModel> Waitlists { get; set; }
        public DbSet<TransactionHistoryModel> TransactionHistories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);

                // Foreign key relationships
                modelBuilder.Entity<TransactionHistoryModel>()
                    .HasOne(th => th.UserPackage)
                    .WithMany()
                    .HasForeignKey(th => th.upId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<TransactionHistoryModel>()
                    .HasOne(th => th.Class)
                    .WithMany()
                    .HasForeignKey(th => th.classId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<WaitlistModel>()
                    .HasOne(w => w.Booking)
                    .WithMany()
                    .HasForeignKey(w => w.bookingId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<WaitlistModel>()
                    .HasOne(w => w.Status)
                    .WithMany()
                    .HasForeignKey(w => w.statusId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<BookingModel>()
                    .HasOne(b => b.User)
                    .WithMany()
                    .HasForeignKey(b => b.userId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<BookingModel>()
                    .HasOne(b => b.Class)
                    .WithMany()
                    .HasForeignKey(b => b.classId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<BookingModel>()
                    .HasOne(b => b.Status)
                    .WithMany()
                    .HasForeignKey(b => b.statusId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<BookingModel>()
                    .HasOne(b => b.UserPackage)
                    .WithMany()
                    .HasForeignKey(b => b.upId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<UserPackageModel>()
                    .HasOne(up => up.User)
                    .WithMany()
                    .HasForeignKey(up => up.userId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<UserPackageModel>()
                    .HasOne(up => up.Package)
                    .WithMany()
                    .HasForeignKey(up => up.packageId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<PackageModel>()
                    .HasOne(p => p.Country)
                    .WithMany()
                    .HasForeignKey(p => p.countryId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<PackageModel>()
                    .Property(p => p.packagePrice)
                    .HasColumnType("decimal(18, 4)");

                modelBuilder.Entity<ClassModel>()
                    .HasOne(c => c.Country)
                    .WithMany()
                    .HasForeignKey(c => c.countryId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<UserModel>()
                    .HasOne(u => u.Country)
                    .WithMany()
                    .HasForeignKey(u => u.countryId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Adding indexes
                modelBuilder.Entity<BookingModel>()
                    .HasIndex(b => b.userId)
                    .HasDatabaseName("IX_Booking_UserId");

                modelBuilder.Entity<WaitlistModel>()
                    .HasIndex(w => w.bookingId)
                    .HasDatabaseName("IX_Waitlist_BookingId");

                modelBuilder.Entity<TransactionHistoryModel>()
                    .HasIndex(th => th.upId)
                    .HasDatabaseName("IX_TransactionHistory_UserPackageId");

                modelBuilder.Entity<UserPackageModel>()
                    .HasIndex(up => up.userId)
                    .HasDatabaseName("IX_UserPackage_UserId");

                modelBuilder.Entity<PackageModel>()
                    .HasIndex(p => p.countryId)
                    .HasDatabaseName("IX_Package_CountryId");

                modelBuilder.Entity<ClassModel>()
                    .HasIndex(c => c.countryId)
                    .HasDatabaseName("IX_Class_CountryId");

                modelBuilder.Entity<UserModel>()
                    .HasIndex(u => u.countryId)
                    .HasDatabaseName("IX_User_CountryId");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
