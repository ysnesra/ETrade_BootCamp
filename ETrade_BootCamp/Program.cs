using ETrade_BootCamp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //�uanki �al��an exe dosyas�n� al bunun i�erisinde tara.Arad���nda AutoMapperConfig class�n� bulacak 

builder.Services.AddDbContext<NorthwindContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //Service i tan�mlad�k /appsettings.json dan okuyup bize database i verecek (VisualStudionun kendi depency mekanizmas� olu�turup) 
    //opts.UseLazyLoadingProxies();   //ili�kili tablolar� otomatik �ekmemizi sa�lar
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

//Ayr�nt�l� a��klamal� link vermek istersek;
//localhost:55324/steven-buchanan-siparisleri-5
app.MapControllerRoute(
    name: "default",
    pattern: "{name}-{surname}-siparisleri-{id}",
    defaults: new {controller="Order", action="Index"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
