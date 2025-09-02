using fullstackBackend.Domain.Entities;
using fullstackBackend.Infraestructure.Data.Seed;
using Microsoft.EntityFrameworkCore;

namespace fullstackBackend.Infraestructure.Data
{
   public class EmployeeContext : DbContext
   {
      public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

      public DbSet<EmployeeEntity> Employees { get; set; }
      public DbSet<DepartmentEntity> Departments { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {

         modelBuilder.Entity<DepartmentEntity>()
             .HasData(DepartmentDataSeed.GetDepartments());

         modelBuilder.Entity<EmployeeEntity>()
             .HasData(EmployeeDataSeed.GetEmployees());
      }
   }
}
