using FlightReservation.MVC.Context;
using FlightReservation.MVC.Models;
using FlightReservation.MVC.Repositories;
using FlightReservation.MVC.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;


#region DI
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<UserRepository>();

#region Localization
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options=> options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName ?? "");
        return factory.Create(nameof(SharedResource), assemblyName.Name ?? "");
    });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR")
    };

    options.DefaultRequestCulture = new RequestCulture(culture:"tr-TR", uiCulture: "tr-TR");
    options.SupportedCultures = supportCultures;
    options.SupportedUICultures = supportCultures;
    options.RequestCultureProviders.Insert(0,new QueryStringRequestCultureProvider());
});
#endregion

#region Context
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
#endregion

#region Authentication
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth",configuration =>
{
    configuration.AccessDeniedPath = "/Account/Login";
    configuration.LoginPath = "/Account/Login";
    configuration.ExpireTimeSpan = TimeSpan.FromHours(1);
    configuration.Cookie.Name="UserLoginCookie";
});
#endregion

#region UI
builder.Services.AddControllersWithViews();
#endregion

#endregion


#region Middleware
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

#region Localization
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
#endregion

app.UseRouting();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //Ýlk Defa Kullanýcý Oluþturuyorsak, Kullanýcý oluþturduktan sonra Role'e bakýyoruz. Eðer Role yoksa oluþturuyoruz.Ardýndan Kullanýcýya Admin Rolü veriyoruz.
    if (!context.Set<User>().Any(p => p.Email == "c11112222@sakarya.edu.tr")) // Any metodu ile veritabanýnda bu maille kayýtlý bir kullanýcý var mý diye kontrol ediyoruz.
    {
        User user = new User()
        {
            FirstName = "Cagla",
            LastName = "Tunc Savas",
            Email = "c11112222@sakarya.edu.tr",
            Password = "sau123",
        };

        context.Set<User>().Add(user);

        Role? role = context.Set<Role>().Where(p => p.Name == "Admin").FirstOrDefault();

        if (role is null)
        {

            role = new Role()
            {
                Name = "Admin"
            };

            context.Set<Role>().Add(role);
        }

        context.Set<UserRole>().Add(new()
        {
            RoleId = role.Id,
            UserId = user.Id
        });

        context.SaveChanges();
    }
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
#endregion











