using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;

namespace RepositoryLayer.Service
{
    public class AddressBookDBContext : DbContext
    {
        public AddressBookDBContext(DbContextOptions<AddressBookDBContext> options)
            : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AddressBookEntityEntry> AddressBookEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
