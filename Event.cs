namespace EventManagementAPI.Models{
    public class Event{
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now.AddDays(7);
        public string Category { get; set; } = "Race Day";
        public int ExpectedAttendees { get; set; } = 100;
        public decimal TicketPrice { get; set; } = 150;
        public string AICategorySuggestion { get; set; } = "";
        public double AIPopularityScore { get; set; } = 8.0;
        public string AITags { get; set; } = "";
    }
    public class AIAnalysisResult{
        public string SuggestedCategory { get; set; } = "";
        public double PopularityScore { get; set; }
        public List<string> Tags { get; set; } = new();
        public string GeneratedDescription { get; set; } = "";
        public string Sentiment { get; set; } = "";
        public List<string> Recommendations { get; set; } = new();
    }
}
