using Accounting.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public DbSet<Accrual> Accruals { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<WorkDay> WorkDays { get; set; }
        public DbSet<Deducation> Deducations { get; set; }
        public DbSet<DeducationDocument> DeducationDocuments { get; set; }
    }
}
