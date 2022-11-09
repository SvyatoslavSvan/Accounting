using Accounting.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DAL.Contexts
{
    public class ApplicationDBContext : DbContext
    {
#nullable disable
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opts) : base(opts)
        {

        }
        public DbSet<NotBetEmployee> NotBetEmployees { get; set; }
        public DbSet<BetEmployee> BetEmployees { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<PayoutNotBetEmployee> AccrualsNotBetEmployee { get; set; }
        public DbSet<PayoutBetEmployee> AccrualsBetEmployee { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
    }
}
