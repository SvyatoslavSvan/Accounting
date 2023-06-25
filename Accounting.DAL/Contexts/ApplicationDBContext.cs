using Accounting.Domain.Models;
using Accounting.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DAL.Contexts
{
    public class ApplicationDBContext : DbContext
    {
#nullable disable
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opts) : base(opts)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Payout> Payouts { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
    }
}
