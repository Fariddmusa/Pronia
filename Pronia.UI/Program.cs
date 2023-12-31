using Microsoft.EntityFrameworkCore;
using Pronia.Business.Mappers;
using Pronia.Business.Services.Implementations;
using Pronia.Business.Services.Interfaces;
using Pronia.DataAccess.Contexts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(SliderProfile).Assembly);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
