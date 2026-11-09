using Microsoft.AspNetCore.Mvc;
using EventManagementAPI.Models;
using EventManagementAPI.Services;
namespace EventManagementAPI.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase{
        private readonly EventService _svc;
        public EventsController(EventService svc)=>_svc=svc;
        [HttpGet] public async Task<ActionResult<List<Event>>> GetAll()=>Ok(await _svc.GetAllAsync());
        [HttpGet("{id}")] public async Task<ActionResult<Event>> Get(int id){ var e=await _svc.GetByIdAsync(id); if(e==null) return NotFound(); return Ok(e); }
        [HttpPost] public async Task<ActionResult<Event>> Create(Event ev){ try{ var created=await _svc.CreateAsync(ev); return CreatedAtAction(nameof(Get), new{id=created.Id}, created); }catch(ArgumentException ex){ return BadRequest(ex.Message); } }
        [HttpGet("category/{cat}")] public async Task<ActionResult<List<Event>>> ByCat(string cat)=>Ok(await _svc.GetByCategoryAsync(cat));
    }
}
