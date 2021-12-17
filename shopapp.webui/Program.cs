using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using shopapp.business.Abstract;
using shopapp.business.Concrete;
using shopapp.dataAccess.Abstract;
using shopapp.dataAccess.Concrete.EFCore;
using shopapp.webui.Identity;
using shopapp.webui.MailService;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment env = builder.Environment;

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductRepo, EFCoreProductRepo>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryRepo, EFCoreCategoriesRepo>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();


builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(@"Data Source=.\SQLEXPRESS;Database=Shop;Integrated Security=SSPI;"));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i => new SmtpEmailSender(
    configuration["EmailSender:Host"],
    configuration.GetValue<int>("EmailSender:Port"),
    configuration.GetValue<bool>("EmailSender:EnableSSL"),
    configuration["EmailSender:UserName"],
    configuration["EmailSender:Password"]
));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;


});


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".Shopapp.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});

var app = builder.Build();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = "/modules"
});


if (env.IsDevelopment())
{
    SeedDatabase.Seed();
    app.UseDeveloperExceptionPage();
}

app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "adminProducts",
                    pattern: "/admin/products",
                    defaults: new { controller = "Admin", action = "productList" }
                );

                endpoints.MapControllerRoute(
                    name: "adminProducts",
                    pattern: "/admin/categories",
                    defaults: new { controller = "Admin", action = "categoryList" }
                );

                endpoints.MapControllerRoute(
                    name: "adminProducts",
                    pattern: "/admin/products/{id?}",
                    defaults: new { controller = "Admin", action = "editProduct" }
                );

                endpoints.MapControllerRoute(
                    name: "adminProducts",
                    pattern: "/admin/categories/{id?}",
                    defaults: new { controller = "Admin", action = "editCategory" }
                );

                // localhost/search    
                endpoints.MapControllerRoute(
                    name: "search",
                    pattern: "search",
                    defaults: new { controller = "Home", action = "search" }
                );

                endpoints.MapControllerRoute(
                    name: "products",
                    pattern: "products/{category?}",
                    defaults: new { controller = "Shop", action = "list" }
                                );
                endpoints.MapControllerRoute(
                    name: "productdetails",
                    pattern: "{url}",
                    defaults: new { controller = "Shop", action = "details" }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
app.Run();
