namespace EventManagementMVC.Models{
 public class EventViewModel{ public int Id{get;set;} public string Name{get;set;}=""; public string Description{get;set;}=""; public string Location{get;set;}=""; public DateTime Date{get;set;}=DateTime.Now.AddDays(7); public string Category{get;set;}="Race Day"; public int ExpectedAttendees{get;set;}=100; public decimal TicketPrice{get;set;}=150; public string ImageUrl{get;set;}="/images/color-run.jpg"; public string AICategorySuggestion{get;set;}=""; public double AIPopularityScore{get;set;}=8.5; public string AITags{get;set;}=""; }
 public class AnalyzeRequestModel{ public string Name{get;set;}=""; public string Description{get;set;}=""; }
 public class AIResult{ public string SuggestedCategory{get;set;}=""; public double PopularityScore{get;set;} public List<string> Tags{get;set;}=new(); public string GeneratedDescription{get;set;}=""; }
}
