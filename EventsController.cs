using Microsoft.AspNetCore.Mvc; using EventManagementMVC.Models; using EventManagementMVC.Services;
namespace EventManagementMVC.Controllers{
 public class EventsController:Controller{
  private readonly InMemoryEventService _local; private readonly ApiClient _api;
  public EventsController(InMemoryEventService local, ApiClient api){ _local=local; _api=api; }
  public async Task<IActionResult> Index(string? category, string? viewMode){
   var apiData=await _api.TryGetFromApiAsync();
   var events=apiData ?? _local.GetAll();
   if(!string.IsNullOrEmpty(category)) events=events.Where(x=>x.Category==category).ToList();
   ViewBag.Category=category; ViewBag.ViewMode=viewMode??"grid"; return View(events);
  }
  public IActionResult Details(int id){
   var ev=_local.GetById(id) ?? _local.GetAll().FirstOrDefault();
   if(ev==null) return NotFound();
   ViewBag.Similar=_local.GetAll().Where(x=>x.Id!=ev.Id && x.Category==ev.Category).Take(2).ToList();
   return View(ev);
  }
  public IActionResult Create()=>View(new EventViewModel{ Date=DateTime.Now.AddDays(7), ImageUrl="/images/color-run.jpg" });
  [HttpPost]
  public async Task<IActionResult> Create(EventViewModel m){
   if(!ModelState.IsValid) return View(m);
   var ai=_local.Analyze(m.Name, m.Description);
   m.AICategorySuggestion=ai.SuggestedCategory; m.AIPopularityScore=ai.PopularityScore; m.AITags=string.Join(",", ai.Tags);
   var apiCreated=await _api.CreateViaApiAsync(m);
   if(apiCreated!=null) return RedirectToAction(nameof(Details), new{id=apiCreated.Id});
   var created=_local.Add(m);
   return RedirectToAction(nameof(Details), new{id=created.Id});
  }
  [HttpPost] public IActionResult AnalyzeAjax([FromBody] AnalyzeRequestModel r){ var res=_local.Analyze(r.Name, r.Description); return Json(res); }
 }
}
