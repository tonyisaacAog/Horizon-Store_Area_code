using Horizon.Areas.Settings.Models;
using Horizon.Data;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamar((context, registry) =>
{



    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    registry.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    registry.AddDatabaseDeveloperPageExceptionFilter();

    registry.AddAutoMapper(typeof(MyInfrastructure.Mapping.AutoMapping));

    registry.AddControllersWithViews().
                                AddNewtonsoftJson(opt => opt.SerializerSettings
                                .ContractResolver = new DefaultContractResolver());


    registry.AddRazorPages().AddRazorRuntimeCompilation();

    registry.AddControllersWithViews(options =>
    {
        //var policy = new AuthorizationPolicyBuilder()
        //       .RequireAuthenticatedUser()
        //       .Build();
        ////options.ModelBinderProviders.Insert(0,
        ////new CustomModelBinderProvider());
        //options.Filters.Add(new AuthorizeFilter(policy));

    });



    registry.AddIdentity<ApplicationUser, IdentityRole>(options => {
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
     
    }).AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();



    registry.Scan(s =>
    {
        s.TheCallingAssembly();
        s.WithDefaultConventions();
    });

});


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

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute
    (name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute
    (name: "default", pattern: "/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});

app.Run();
