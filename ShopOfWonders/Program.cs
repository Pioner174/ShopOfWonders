using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SOW.DataModels;
using SOW.ShopOfWonders.ExternalServices.RabbitMq;
using SOW.ShopOfWonders.Models;
using SOW.ShopOfWonders.Models.Interfaces;
using SOW.ShopOfWonders.Models.Services;
using Swashbuckle.AspNetCore.Swagger;

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo { Title = "ShopOfWondersApi", Version = "v1" });
});

/// Сервисы для работы с БД
builder.Services.AddScoped<IUserConnector, EFUserService>();
builder.Services.AddScoped<IProductService, EFProductService>();
builder.Services.AddScoped<ITagService, EFTagService>();

///  Сервис для работы с RabbitMQ
builder.Services.AddScoped<IRabbitMq, RabbitMqService>();


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();    // аутентификация
app.UseAuthorization();     // авторизация

app.MapControllers();

app.MapDefaultControllerRoute();


app.MapRazorPages();
app.MapBlazorHub();//Поддержка Blazor
app.MapFallbackToPage("/_Host");

app.UseSwagger();
app.UseSwaggerUI(opts =>
{
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopOfWondersApi");
});

if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.Run();
