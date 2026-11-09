using System.Net.Http.Json; using System.Text.Json; using EventManagementMVC.Models;
namespace EventManagementMVC.Services{
 public class ApiClient{
  private readonly HttpClient _http; private readonly JsonSerializerOptions _opts=new(){PropertyNameCaseInsensitive=true};
  public ApiClient(HttpClient h)=>_http=h;
  public async Task<List<EventViewModel>?> TryGetFromApiAsync(){ try{ return await _http.GetFromJsonAsync<List<EventViewModel>>("/api/events",_opts); }catch{ return null; } }
  public async Task<EventViewModel?> CreateViaApiAsync(EventViewModel ev){ try{ var r=await _http.PostAsJsonAsync("/api/events",ev); if(!r.IsSuccessStatusCode) return null; var j=await r.Content.ReadAsStringAsync(); return JsonSerializer.Deserialize<EventViewModel>(j,_opts); }catch{ return null; } }
 }
}
