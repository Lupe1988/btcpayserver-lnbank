﻿using BTCPayServer.Lightning;
using LNbank.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LNbank.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Transaction>()
                .Property(e => e.Amount)
                .HasConversion(
                    v => v.MilliSatoshi,
                    v => new LightMoney(v));

            modelBuilder
                .Entity<Transaction>()
                .Property(e => e.AmountSettled)
                .HasConversion(
                    v => v.MilliSatoshi,
                    v => new LightMoney(v));

            modelBuilder.Entity<Setting>()
                .HasKey(s => new { s.Name, s.Type });
        }

        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlite("DataSource=app.db");

                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
}
