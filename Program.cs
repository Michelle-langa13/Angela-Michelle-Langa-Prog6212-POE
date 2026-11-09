using Microsoft.EntityFrameworkCore;
using EventManagementAPI.Services;
using EventManagementAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("RaceDayDb"));
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<EventAIService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope()){
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if(!ctx.Events.Any()){
        ctx.Events.AddRange(
            new EventManagementAPI.Models.Event{ Name="Kiddie Go-Kart Derby", Description="Kids racing with colorful helmets", Location="Central Park Loop", Date=DateTime.Now.AddDays(1), Category="Family", ExpectedAttendees=50, TicketPrice=0, AICategorySuggestion="Family", AIPopularityScore=8.5, AITags="family,kids,kart" },
            new EventManagementAPI.Models.Event{ Name="Color Run Sprint", Description="City color run with powder explosions", Location="City Plaza", Date=DateTime.Now.AddDays(2), Category="Race Day", ExpectedAttendees=500, TicketPrice=80, AICategorySuggestion="Race Day", AIPopularityScore=9.2, AITags="color,run,sprint" },
            new EventManagementAPI.Models.Event{ Name="Obstacle Dash Fun", Description="Mud run with walls and nets", Location="Garden Trail", Date=DateTime.Now.AddDays(3), Category="Endurance", ExpectedAttendees=200, TicketPrice=120, AICategorySuggestion="Endurance", AIPopularityScore=8.8, AITags="obstacle,mud" },
            new EventManagementAPI.Models.Event{ Name="Grand Prix Championship", Description="High octane final", Location="Kyalami Circuit", Date=DateTime.Now.AddDays(5), Category="Race Day", ExpectedAttendees=5000, TicketPrice=350, AICategorySuggestion="Race Day", AIPopularityScore=9.5, AITags="grand prix" }
        );
        ctx.SaveChanges();
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

public partial class Program {}
