using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Providers;
using Accounting.Domain.Models;
using Accounting.MigrationApplyer;
using Accounting.Services;
using Accouting.Domain.Managers.Implementations;
using Accouting.Domain.Managers.Interfaces;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"));
});
builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger());
builder.Services.AddUnitOfWork<ApplicationDBContext>();
builder.Services.AddScoped<IPayoutProvider, PayoutProvider>();
builder.Services.AddScoped<IEmployeeProvider, EmployeeProvider>();
builder.Services.AddScoped<IBaseProvider<Group>, GroupProvider>();
builder.Services.AddTransient<IDocumentProvider, DocumentProvider>();
builder.Services.AddTransient<IWorkDayProvider, WorkDayProvider>();
builder.Services.AddScoped<IDocumentManager, DocumentManager>(); 
builder.Services.AddScoped<IPayoutManager, PayoutManager>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
builder.Services.AddScoped<IWorkDayManager, WorkDayManager>();
builder.Services.AddScoped<IGroupManager, GroupManager>();
builder.Services.AddTransient<ISalaryManager, SalaryManager>();
builder.Services.AddScoped<IReportManager, ReportManager>();
builder.Services.AddTransient<ISessionDocumentService, SessionDocumentService>();
builder.Services.AddScoped<ITimesheetProvider, TimesheetProvider>();
builder.Services.AddScoped<ITimesheetManager, TimesheetManager>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
MigrationApplyer.ApplyMigrations(app.Services);
app.Run();

