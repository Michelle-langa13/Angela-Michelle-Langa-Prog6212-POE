using EventManagementMVC.Models;
namespace EventManagementMVC.Services{
 public class InMemoryEventService{
  private List<EventViewModel> _events; int _nextId=7;
  public InMemoryEventService(){
   _events=new List<EventViewModel>{
    new EventViewModel{ Id=1, Name="Kiddie Go-Kart Derby 🎀", Description="Kids racing with bows and helmets, super cute!", Location="Central Park Loop", Date=DateTime.Now.AddDays(1).AddHours(10), Category="Family", ExpectedAttendees=50, TicketPrice=0, ImageUrl="/images/go-kart.jpg", AICategorySuggestion="Family", AIPopularityScore=8.5, AITags="family,kids,kart,fun" },
    new EventViewModel{ Id=2, Name="Color Run Sprint 🌈", Description="City color powder explosion!", Location="City Plaza • 2km", Date=DateTime.Now.AddDays(2).AddHours(13), Category="Race Day", ExpectedAttendees=500, TicketPrice=80, ImageUrl="/images/color-run.jpg", AICategorySuggestion="Race Day", AIPopularityScore=9.2, AITags="color,run,sprint" },
    new EventViewModel{ Id=3, Name="Obstacle Dash Fun ✨", Description="Mud, walls, crawl — community fun", Location="Garden Trail • 1.5km", Date=DateTime.Now.AddDays(3).AddHours(9), Category="Endurance", ExpectedAttendees=200, TicketPrice=120, ImageUrl="/images/mud-run.jpg", AICategorySuggestion="Endurance", AIPopularityScore=8.8, AITags="obstacle,mud" },
    new EventViewModel{ Id=4, Name="Grand Prix Championship 🏆", Description="Final lap showdown", Location="Kyalami Circuit", Date=DateTime.Now.AddDays(5), Category="Race Day", ExpectedAttendees=5000, TicketPrice=350, ImageUrl="/images/color-run.jpg", AICategorySuggestion="Race Day", AIPopularityScore=9.5, AITags="grand prix" },
    new EventViewModel{ Id=5, Name="Night Drag Showdown 🌙", Description="Under lights drag battles", Location="Midvaal", Date=DateTime.Now.AddDays(4).AddHours(19), Category="Drag", ExpectedAttendees=2000, TicketPrice=150, ImageUrl="/images/mud-run.jpg", AICategorySuggestion="Drag", AIPopularityScore=8.9, AITags="drag,night" },
    new EventViewModel{ Id=6, Name="Family Pit Lane 🎀", Description="Pit tours and karts", Location="Zwartkops", Date=DateTime.Now.AddDays(12), Category="Family", ExpectedAttendees=800, TicketPrice=80, ImageUrl="/images/go-kart.jpg", AICategorySuggestion="Family", AIPopularityScore=7.8, AITags="family,pit,fun" }
   };
  }
  public List<EventViewModel> GetAll()=>_events;
  public EventViewModel? GetById(int id)=>_events.FirstOrDefault(e=>e.Id==id);
  public EventViewModel Add(EventViewModel ev){ ev.Id=_nextId++; if(string.IsNullOrEmpty(ev.ImageUrl)) ev.ImageUrl="/images/color-run.jpg"; _events.Add(ev); return ev; }
  public AIResult Analyze(string name, string desc){
   var t=(name+" "+desc).ToLower(); string cat="General"; if(t.Contains("race")||t.Contains("sprint")||t.Contains("grand")) cat="Race Day"; else if(t.Contains("family")||t.Contains("kids")||t.Contains("kart")) cat="Family"; else if(t.Contains("drag")||t.Contains("night")) cat="Drag"; else if(t.Contains("obstacle")||t.Contains("mud")) cat="Endurance";
   var tags=new List<string>(); if(t.Contains("color")) tags.Add("color"); if(t.Contains("kart")) tags.Add("kart"); if(t.Contains("mud")) tags.Add("mud"); if(t.Contains("family")) tags.Add("family"); if(!tags.Any()) tags.Add("cute"); tags.Add("race");
   double score= Math.Clamp((name.Length+desc.Length)%10 + 0.5, 1, 10);
   return new AIResult{ SuggestedCategory=cat, PopularityScore=Math.Round(score,1), Tags=tags, GeneratedDescription="🎀 "+name+" — A super cute "+cat+" experience!" };
  }
 }
}
