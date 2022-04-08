using Microsoft.EntityFrameworkCore;
using StaffWebApp.Models;

namespace StaffWebApp.Context
{
    public class StaffDbContext:DbContext
    {

        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>().HasIndex(d => d.Name).IsUnique();

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
