using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Extensions;
using StudentManagement.BLL.AccountService;
using StudentManagement.BLL.ExamServices;
using StudentManagement.BLL.GroupServices;
using StudentManagement.BLL.QnAServices;
using StudentManagement.BLL.StudentServices;
using StudentManagement.BLL.UtilityServices;
using StudentManagement.Data.DataContext;
using StudentManagement.Data.Unitofwork;
using StudentManagement.UI.FluentValidators;
using StudentManagement.ViewModels.UserViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Custom - Register ConnectionString and ApplicationDbContext as Service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionString"));
});


// Custom - Register BLL Services
builder.Services.AddScoped<IAccountService, AccountService>();
//Custom - Register IUnitOfWork as service
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IQnAService, QnAService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUtilityService, UtilityService>();

// Custom - register authentication scheme for custom filter
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Account/Login;";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Custom - Register Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});

// Fluent Validation Configuration
// Custom - Enables integration between FluentValidation and ASP.NET MVC's automatic validation pipeline.
//builder.Services.AddFluentValidationAutoValidation();
// Custom - Enables integration between FluentValidation and ASP.NET client-side validation.
//builder.Services.AddFluentValidationClientsideAdapters();
// Custom - Registering Model and Validator to show the error message on client side
//builder.Services.AddTransient<IValidator<UserLoginVM>, UserLoginValidator>();

// Custom - Registering Cloudscribe service
builder.Services.AddCloudscribePagination();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Custom - Configure middleware for Session
app.UseSession();

app.UseRouting();

// Custom - use middleware for authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
