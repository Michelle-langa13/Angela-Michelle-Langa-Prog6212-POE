using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace EventManagementAPI.Services{
    public class EventService{
        private readonly AppDbContext _ctx;
        public EventService(AppDbContext ctx)=>_ctx=ctx;
        public async Task<List<Event>> GetAllAsync()=>await _ctx.Events.ToListAsync();
        public async Task<Event?> GetByIdAsync(int id)=>await _ctx.Events.FindAsync(id);
        public async Task<Event> CreateAsync(Event ev){
            if(ev.Date < DateTime.Today) throw new ArgumentException("Date cannot be in past");
            if(string.IsNullOrWhiteSpace(ev.Name)) throw new ArgumentException("Name required");
            if(ev.Description.Length<10) throw new ArgumentException("Description must be >=10 chars");
            _ctx.Events.Add(ev);
            await _ctx.SaveChangesAsync();
            return ev;
        }
        public async Task<List<Event>> GetByCategoryAsync(string cat)=>await _ctx.Events.Where(e=>e.Category==cat).ToListAsync();
    }
}
