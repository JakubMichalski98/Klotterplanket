using Klotterplanket.Data;
using Klotterplanket.Models;
using Klotterplanket.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Member");
});

var connectionString1 = builder.Configuration.GetConnectionString("MessageDbConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString1));

var connectionString2 = builder.Configuration.GetConnectionString("UserDbConnection");
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString2));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Index";
    options.AccessDeniedPath = "/Index";
});

builder.Services.AddScoped<IMessageRepo, MessageRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
