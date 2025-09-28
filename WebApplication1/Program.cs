using InMemoryCrudApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); // Use Razor Pages, not ControllersWithViews for Razor Pages projects
builder.Services.AddSingleton<EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Razor Pages default error page
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

//app.UsePathBase("/Employee");

app.MapRazorPages(); // Map Razor Pages endpoints

// Ensure no custom Kestrel address or PathBase is set in configuration or launchSettings.json
// Remove any ASPNETCORE_URLS or applicationUrl settings that include a path base (e.g., "http://localhost:5000/basepath")

app.Run();