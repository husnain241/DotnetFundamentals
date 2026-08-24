using mini_project_SMS.Services;
using mini_project_SMS.Services.Interfaces;
using mini_project_SMS.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IImageService, ImageService>();  

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // Catches unhandled exceptions and redirects to /Shared/Error or /Home/Error
    app.UseExceptionHandler("/Student/Error"); 
    app.UseHsts();
}
else
{
    // For testing in local Development environment, you can point exception handler to:
    app.UseExceptionHandler("/Student/Error");
    
    // Or keep UseDeveloperExceptionPage() during active debugging
    // app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
