using EventManagementAPI.Services;
using Xunit;
namespace EventManagementAPI.Tests{
    public class EventAIServiceTests{
        private readonly EventAIService _svc = new();
        [Fact] public void Analyze_Race_ReturnsRaceCategory(){
            var r=_svc.Analyze("Grand Prix Championship", "Fast cars on circuit");
            Assert.Equal("Race Day", r.SuggestedCategory);
        }
        [Fact] public void Analyze_Family_ReturnsFamily(){
            var r=_svc.Analyze("Kiddie Go-Kart Derby", "Kids racing with family");
            Assert.Equal("Family", r.SuggestedCategory);
        }
        [Fact] public void Analyze_Endurance_ReturnsEndurance(){
            var r=_svc.Analyze("Obstacle Dash", "Mud run with walls");
            Assert.Equal("Endurance", r.SuggestedCategory);
        }
        [Fact] public void Analyze_ReturnsScoreInRange(){
            var r=_svc.Analyze("Color Run Sprint", "City color run");
            Assert.InRange(r.PopularityScore, 1, 10);
        }
        [Fact] public void Analyze_ReturnsTags(){
            var r=_svc.Analyze("Color Run", "Color powder");
            Assert.NotEmpty(r.Tags);
        }
    }
}
