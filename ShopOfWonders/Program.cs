using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOW.DataModel;
using SOW.ShopOfWonders.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:DataConnection"]);
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
