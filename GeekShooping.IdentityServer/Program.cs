using Duende.IdentityServer.Services;
using GeekShooping.IdentityServer.Configuration;
using GeekShooping.IdentityServer.Initializer;
using GeekShooping.IdentityServer.Models.Context;
using GeekShooping.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("MySQLConnection");
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 32))));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MySQLContext>()
    .AddDefaultTokenProviders();

var identityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
})
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<ApplicationUser>()
    .AddProfileService<ProfileService>();

builder.Services.AddScoped<IDBInitializer, DbInitializer>();
builder.Services.AddScoped<IProfileService, ProfileService>();

identityServer.AddDeveloperSigningCredential();

var app = builder.Build();

var dbInitialize = app.Services.CreateScope().ServiceProvider.GetService<IDBInitializer>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

dbInitialize.Initialize();

app.Run();
