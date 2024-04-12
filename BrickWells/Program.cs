using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BrickWells.Data;
using BrickWells.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.ConsentCookieValue = "true";
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDbContext<BrickContext>(options =>
{
    options.UseSqlite(connectionString);
});

// builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Configure identity options and password requirements and policy
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 10;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
});



builder.Services.AddScoped<IBrickRepository, EFBrickRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<IAdminRepository, EFAdminRepository>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


// Might need to add this back in for the cookie policy to work
// builder.Services.Configure<CookiePolicyOptions>(options =>
// {
//     options.CheckConsentNeeded = context => true;
//     options.MinimumSameSitePolicy = SameSiteMode.None;
// });
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseCookiePolicy();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseSession();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

// app.UseEndpoints(endpoints =>
//     {
//         endpoints.MapRazorPages();
//     });

app.MapControllerRoute(name:"default", pattern:"{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute("pagenumandtype", "{category}/{pageNum}", new {Controller = "Home", action = "Products" }); 
app.MapControllerRoute("pagination", "{pageNum}", new { Controller = "Home", action = "Products", pageNum = 1 });
app.MapControllerRoute("category", "{category}", new { Controller = "Home", action = "Products", pageNum = 1 });
app.MapDefaultControllerRoute();

app.MapRazorPages();

// Seed the user database with roles
using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@team2-8.com";
    string password = "TooRadToB3Sad!";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();