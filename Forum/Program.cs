using Forum.Data;
using Forum.Data.Interfaces;
using Forum.ForumServises;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "";
//builder.WebHost.ConfigureAppConfiguration((builderContext, config) => {
//    IWebHostEnvironment env = builderContext.HostingEnvironment;
//    config.AddJsonFile("storageAzureSettings.json", optional: false, reloadOnChange: true)
//});


builder.Configuration.AddJsonFile(
     "storageAzureSettings.json",
     optional: false,
     reloadOnChange: true);


if (builder.Environment.IsProduction())
{
    // Add services to the container.
     connectionString = builder.Configuration.GetConnectionString("ForumDBConnectionProduct") ?? throw new InvalidOperationException("Connection string 'ForumDBConnectionProduct' not found.");
}
else if (builder.Environment.IsDevelopment())
{
    // Add services to the container.
      connectionString = builder.Configuration.GetConnectionString("ForumDBConnection") ?? throw new InvalidOperationException("Connection string 'ForumDBConnection' not found.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddScoped<IForums, ForumService>();
builder.Services.AddScoped<IPosts, PostService>();
builder.Services.AddScoped<IUpload, UploadService>();
builder.Services.AddScoped<IApplicationUser, ApplicationUserService>();

builder.Services.AddTransient<DataSeeder>();


builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
//else
//{
//    //app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    //app.UseHsts();
//}

app.UseHttpsRedirection();

//Добаляем пользователя администратора, если его нет в базе данных
SeedDatabase(); 

app.UseStaticFiles();


void SeedDatabase() 
{
    var task = Task.Run(() =>
    {
        using (var scope = app.Services.CreateScope())
        {
            var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
            dataSeeder.SeedSuperUser();
        }
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
