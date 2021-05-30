
using Microsoft.EntityFrameworkCore;
using Overtime_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Context
{
    public class OvertimeContext : DbContext
    {
        public OvertimeContext(DbContextOptions<OvertimeContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}

        public DbSet<Person> Person { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<RoleAccount> RoleAccount { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Overtime> Overtime { get; set; }
        public DbSet<Kind> Kind { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleAccount>()
                .HasKey(ar => new { ar.NIK, ar.RoleId });
            modelBuilder.Entity<RoleAccount>()
                .HasOne(ar => ar.Account)
                .WithMany(r => r.RoleAccounts)
                .HasForeignKey(ar => ar.NIK);
            modelBuilder.Entity<RoleAccount>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.RoleAccounts)
                .HasForeignKey(ar => ar.RoleId);
        }
    }
}
