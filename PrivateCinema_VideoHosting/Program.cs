using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PrivateCinema_VideoHosting.Data;
using PrivateCinema_VideoHosting.Hubs;
using PrivateCinema_VideoHosting.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSignalR();

builder.Services.AddTransient<UserService>(); // создаетс€ каждый раз когда делаетс€ запрос
builder.Services.AddScoped<DatabaseService>(); // создаетс€ один раз

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // ƒобавл€ем севис по аутификации
    .AddCookie(options =>
    {
        options.LoginPath = "/Authorization/Login";
        options.Cookie.Name = "LoginCookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(3);
    });

builder.WebHost.ConfigureKestrel(options =>   
{
    options.Limits.MaxRequestBodySize = 2000 * 1024 * 1024;
});
builder.Services.Configure<FormOptions>(options =>    
{
    options.MultipartBodyLengthLimit = 512 * 2024 * 1024;
});


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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy();

app.MapPost("upload", async (string connectionId, IHubContext<CinemaHub> hubContext) =>
{
    await hubContext.Clients.Client(connectionId).SendAsync("Refresh");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorization}/{action=Login}/{id?}");

app.MapHub<CinemaHub>("/cinemaHub");

app.Run();
