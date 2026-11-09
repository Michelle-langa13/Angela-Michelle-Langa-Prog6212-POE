using Microsoft.AspNetCore.Mvc; using EventManagementMVC.Services;
namespace EventManagementMVC.Controllers{
 public class HomeController:Controller{
  private readonly InMemoryEventService _local; private readonly ApiClient _api;
  public HomeController(InMemoryEventService local, ApiClient api){ _local=local; _api=api; }
  public async Task<IActionResult> Index(string? filter){
   var apiData=await _api.TryGetFromApiAsync();
   var events=apiData ?? _local.GetAll();
   if(!string.IsNullOrEmpty(filter)) events=events.Where(x=>x.Category.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
   ViewBag.Filter=filter; ViewBag.Count=events.Count; ViewBag.TotalRacers=events.Sum(x=>x.ExpectedAttendees);
   ViewBag.ApiStatus=apiData!=null ? "✨ Connected to API" : "🧁 MVC-only mode (runs alone)";
   return View(events);
  }
  public IActionResult Login()=>View();
  public IActionResult Signup()=>View();
  public IActionResult Error()=>View();
 }
}
