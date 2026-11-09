using EventManagementAPI.Models;
namespace EventManagementAPI.Services{
    public class EventAIService{
        public AIAnalysisResult Analyze(string name, string description){
            var text = (name + " " + description).ToLower();
            string category = "General";
            if(text.Contains("race") || text.Contains("sprint") || text.Contains("grand") || text.Contains("circuit")) category="Race Day";
            else if(text.Contains("family") || text.Contains("kids") || text.Contains("kart")) category="Family";
            else if(text.Contains("drag") || text.Contains("night")) category="Drag";
            else if(text.Contains("obstacle") || text.Contains("mud") || text.Contains("endurance")) category="Endurance";

            var tags = new List<string>();
            if(text.Contains("color")) tags.Add("color");
            if(text.Contains("kart")) tags.Add("kart");
            if(text.Contains("mud") || text.Contains("obstacle")) tags.Add("obstacle");
            if(text.Contains("family")) tags.Add("family");
            if(text.Contains("race") || text.Contains("grand prix")) tags.Add("race");
            if(!tags.Any()) tags.Add("event");

            var score = Math.Clamp(((name.Length + description.Length) % 10) + 0.5, 1, 10);
            var desc = "🏁 " + name + " — An adrenaline-fueled " + category + " experience. Expect checkered flags!";

            return new AIAnalysisResult{
                SuggestedCategory=category,
                PopularityScore=Math.Round(score,1),
                Tags=tags,
                GeneratedDescription=desc,
                Sentiment="Positive",
                Recommendations=new List<string>{ "Add more racers", "Promote on social", "Check weather" }
            };
        }
        public List<Event> RecommendSimilar(Event current, List<Event> all){
            return all.Where(e=>e.Id!=current.Id && e.Category==current.Category).Take(2).ToList();
        }
    }
}
