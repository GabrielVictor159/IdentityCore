using IdentityCore.Areas.Identity.Data;
using IdentityCore.Data;
using IdentityCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityCoreContext>(options => options.UseNpgsql(Environment.GetEnvironmentVariable("DBCONN")));
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<IdentityCoreContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IMyEmailSender, MyEmailSender>();

builder.Services.AddRazorPages();


var app = builder.Build();
var optionsContext = new DbContextOptions<IdentityCoreContext>();
using (var context = new IdentityCoreContext(optionsContext))
{
    try
    {
        context.Database.Migrate();
    }
    catch { }
}
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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
