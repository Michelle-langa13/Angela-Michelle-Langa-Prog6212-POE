using EventManagementMVC.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var apiUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? Environment.GetEnvironmentVariable("API_URL") ?? "https://localhost:7001";
builder.Services.AddHttpClient<ApiClient>(c => {
    try { c.BaseAddress = new Uri(apiUrl); } catch { c.BaseAddress = new Uri("https://localhost:7001"); }
    c.Timeout = TimeSpan.FromSeconds(5);
});
builder.Services.AddSingleton<InMemoryEventService>();
var app = builder.Build();
if (!app.Environment.IsDevelopment()){ app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name:"default", pattern:"{controller=Home}/{action=Index}/{id?}");
app.Run();
