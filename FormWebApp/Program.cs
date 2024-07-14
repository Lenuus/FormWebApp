using FormWebApp.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FormApp.Application;
using FormApp.Common.Helpers;
using FormApp.Domain;
using FormWebApp.Persistence.Interceptors;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<MainDbContext>(
            (sp, option) => option
                            .UseSqlServer(builder.Configuration.GetConnectionString("MainDbContext"))
                                                                                   .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
                                                                                   .AddInterceptors(sp.GetRequiredService<CreateInterceptor>()));

        AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(f => f.GetTypes())
            .Where(f => typeof(IApplicationService).IsAssignableFrom(f) && !f.IsInterface)
            .ToList().ForEach(f => builder.Services.AddTransient(f.GetInterface($"I{f.Name}"), f));

        builder.Services.AddScoped<SoftDeleteInterceptor>();
        builder.Services.AddScoped<CreateInterceptor>();
        builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddRazorPages();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<MainDbContext>();

        //builder.Services.AddIdentity<User, Role>()
        //        .AddEntityFrameworkStores<MainDbContext>()
        //        .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });



        builder.Services.AddControllersWithViews();


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

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Form}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();
    }
}