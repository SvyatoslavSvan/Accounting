using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Providers;
using Accounting.Domain.Models;
using Accounting.Services;
using Accounting.Services.Interfaces;
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
builder.Services.AddTransient<ISessionDeducationDocumentService, SessionDeducationDocumentService>();
builder.Services.AddTransient<IDeducationDocumentProvider, DeducationDocumentProvider>();
builder.Services.AddTransient<IDeducationProvider, DeducationProvider>();
builder.Services.AddScoped<IAccrualProvider, AccrualProvider>();
builder.Services.AddScoped<IEmployeeProvider, EmployeeProvider>();
builder.Services.AddScoped<IBaseProvider<Group>, GroupProvider>();
builder.Services.AddTransient<IDocumentProvider, DocumentProvider>();
builder.Services.AddTransient<IWorkDayProvider, WorkDayProvider>();
builder.Services.AddTransient<ISessionDocumentService, SessionDocumentService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddScoped<IDeducationDocumentManager, DeducationDocumentManager>();
builder.Services.AddScoped<IDeducationManager, DeducationManager>();
builder.Services.AddScoped<IDocumentManager, DocumentManager>(); 
builder.Services.AddScoped<IAccrualManager, AccrualManager>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
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

app.Run();
