using GameStore.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<GameStoreDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => { options.LoginPath = "/Login"; });

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<GameStore.Business.Interfaces.IGameService,
    GameStore.Business.Services.GameService>();

builder.Services.AddScoped<GameStore.Data.Repositories.IGameRepository,
    GameStore.Data.Repositories.GameRepository>();

builder.Services.AddScoped<GameStore.Business.Interfaces.ICategoryService,
    GameStore.Business.Services.CategoryService>();

builder.Services.AddScoped<GameStore.Data.Repositories.ICategoryRepository,
    GameStore.Data.Repositories.CategoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Admin", "Manager", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    var sampleUsers = new List<(string Email, string Role)>
    {
        ("admin@example.com", "Admin"),
        ("manager@example.com", "Manager"),
        ("user@example.com", "User"),
    };

    foreach (var (email, role) in sampleUsers)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new IdentityUser { UserName = email, Email = email };
            string password = $"{role}@123";
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
app.Run();
