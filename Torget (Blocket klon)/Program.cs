using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Torget__Blocket_klon_.Areas.Konto.Pages;
using Torget__Blocket_klon_.Data.Models;
using Torget__Blocket_klon_.Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddDbContext<TorgetDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddScoped<Database>();
builder.Services.AddScoped<AdHandler>();
builder.Services.AddScoped<ZipCodeHandler>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ChatService>();

#region Identity

builder.Services.AddDefaultIdentity<TorgetUser>(options =>
        options.User.RequireUniqueEmail = true)
    .AddEntityFrameworkStores<TorgetDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(12);
    options.LoginPath = "/Konto/Inloggning";
    options.SlidingExpiration = true;
    options.ReturnUrlParameter = "ReturnUrl";
});

#endregion

builder.Services.AddSignalR(opt => opt.EnableDetailedErrors = true);


var app = builder.Build();


// när vi är i "debug" läge
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<Database>();
    app.UseDeveloperExceptionPage();

    await db.RecreateAndSeedDatabase();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChattHub>("/chatHub");
app.MapBlazorHub();

app.Run();