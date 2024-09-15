using EmployeeManagement.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository.DatabaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
    {
    }
    public DbSet<Employee> Employees { get; set; }

}
