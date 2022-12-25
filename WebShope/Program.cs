using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebShope.DAL;
using WebShope.DAL.Interfaces;
using WebShope.DAL.Repository;
using WebShope.Service.Interfaces;
using WebShope.Service.Realization;



var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("FirstConnectionString");

builder.Services.Configure<Account>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
{
    options.LoginPath = "/user/login";
}); ;

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("defalt", "{controller=Home}/{action=Index}/{id?}");


app.Run();
