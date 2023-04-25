using ETrade_BootCamp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //Þuanki çalýþan exe dosyasýný al bunun içerisinde tara.Aradýðýnda AutoMapperConfig classýný bulacak 

builder.Services.AddDbContext<NorthwindContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    //Service i tanýmladýk /appsettings.json dan okuyup bize database i verecek (VisualStudionun kendi depency mekanizmasý oluþturup) 
    //opts.UseLazyLoadingProxies();   //iliþkili tablolarý otomatik çekmemizi saðlar
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

//Ayrýntýlý açýklamalý link vermek istersek;
//localhost:55324/steven-buchanan-siparisleri-5
app.MapControllerRoute(
    name: "default",
    pattern: "{name}-{surname}-siparisleri-{id}",
    defaults: new {controller="Order", action="Index"});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
