using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
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

#region Identity

builder.Services.AddDefaultIdentity<TorgetUser>(options =>
        options.User.RequireUniqueEmail = true)
    .AddEntityFrameworkStores<TorgetDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    //TODO: Fixa r�tt paths
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); //TODO: Hur l�nge vill vi ha?
    options.LoginPath = "/User/Login";
    options.SlidingExpiration = true;
    options.ReturnUrlParameter = "ReturnUrl";
    options.AccessDeniedPath = "/AccessDenied";
});

#endregion


var app = builder.Build();


// n�r vi �r i "debug" l�ge
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
app.MapBlazorHub();

app.Run();