using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOW.DataModel;
using SOW.ShopOfWonders.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<IdentityContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:DataConnection"]);
});

builder.Services.Configure<IdentityOptions>(opts =>
{
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;

    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters += "!";
});

builder.Services.AddIdentity<User, IdentityRole<long>>()
    .AddEntityFrameworkStores<IdentityContext>();



var app = builder.Build();

app.UseStaticFiles();

app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();
