using Microsoft.Extensions.FileProviders;
using shopapp.business.Abstract;
using shopapp.business.Concrete;
using shopapp.dataAccess.Abstract;
using shopapp.dataAccess.Concrete.EFCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepo, EFCoreProductRepo>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryRepo, EFCoreCategoriesRepo>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
var app = builder.Build();
app.UseRouting();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = "/modules"
});
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment env = builder.Environment;

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
