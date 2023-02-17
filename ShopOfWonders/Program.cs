using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOW.DataModels;
using SOW.ShopOfWonders.Models;
using SOW.ShopOfWonders.Models.Interfaces;
using SOW.ShopOfWonders.Models.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages(opts =>
{
    opts.RootDirectory = "/Blazor/Pages";
});
builder.Services.AddServerSideBlazor(opts =>
{
#if DEBUG
    opts.DetailedErrors = true;
#endif
});

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

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie(opts =>
{
    opts.LoginPath = "/mvc/account/LogIn";
});

builder.Services.AddTransient<IUserConnector, EFUserReposytory>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();    // ��������������
app.UseAuthorization();     // �����������


app.MapControllers();

app.MapDefaultControllerRoute();

app.MapRazorPages();
app.MapBlazorHub();//��������� Blazor
app.MapFallbackToPage("/_Host");

app.Run();
