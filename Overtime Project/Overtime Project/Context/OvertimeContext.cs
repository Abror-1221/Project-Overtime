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
    }
}
