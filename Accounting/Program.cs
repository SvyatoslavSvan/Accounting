using Accounting.DAL.Contexts;
using Accounting.DAL.Interfaces;
using Accounting.DAL.Interfaces.Base;
using Accounting.DAL.Providers;
using Accounting.DAL.Repositories;
using Accounting.Domain.Models;
using Accounting.Services;
using Accounting.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"));
});
builder.Services.AddScoped<IBaseEmployeeRepository<BetEmployee>, BetEmployeeRepository>();
builder.Services.AddScoped<IBaseEmployeeRepository<NotBetEmployee> ,NotBetEmployeeRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IBaseRepository<Accrual>, AccrualRepository>();
builder.Services.AddScoped<IBaseProvider<Accrual>, AccrualProvider>();
builder.Services.AddTransient<IBaseRepository<Document>, DocumentRepository>();
builder.Services.AddScoped<IEmployeeProvider, EmployeeProvider>();
builder.Services.AddScoped<IBaseProvider<Group>, GroupProvider>();
builder.Services.AddTransient<IBaseProvider<Document>, DocumentProvider>();
builder.Services.AddTransient<ISessionDocumentService, SessionDocumentService>();
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
