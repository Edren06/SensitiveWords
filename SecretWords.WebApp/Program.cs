using Microsoft.Extensions.Options;
using SecretWords.WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Bind ApiSettings from configuration
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register ApiService
builder.Services.AddHttpClient<ApiService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
