using AspNetCoreFundamentals.Configuration;
using AspNetCoreFundamentals.Middleware;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var appName =
    builder.Configuration["ApplicationSettings:ApplicationName"];

var version =
    builder.Configuration["ApplicationSettings:Version"];

var apiUrl =
    builder.Configuration["ExternalServices:ApiUrl"];



var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Configure<ApplicationSettings>(
    builder.Configuration.GetSection("ApplicationSettings"));

Console.WriteLine($"Application Name: {appName}");
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();


}
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Use(async (context, next) =>
{
    var stopwatch = Stopwatch.StartNew();

    await next();

    stopwatch.Stop();

    Console.WriteLine(
        $"Request: {context.Request.Path} | Time: {stopwatch.ElapsedMilliseconds} ms");
});
app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Incoming Request: {context.Request.Path}");

    await next();

    Console.WriteLine($"Outgoing Response: {context.Response.StatusCode}");
});
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
