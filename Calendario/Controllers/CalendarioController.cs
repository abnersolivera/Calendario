using Calendario.Servico;
using Microsoft.AspNetCore.Mvc;

namespace Calendario.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarioController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleCalendarService _calendarService;

        public CalendarioController(HttpClient httpClient, GoogleCalendarService calendarService)
        {
            _httpClient = httpClient;
            _calendarService = calendarService;
        }

        [HttpGet("{calendarId}")]
        public async Task<IActionResult> GetCalendar(string calendarId)
        {
            var calendar = await _calendarService._calendarService.Calendars.Get(calendarId).ExecuteAsync();
            return Ok(calendar);
        }

        [HttpGet("{calendarId}/events")]
        public async Task<IActionResult> GetCalendarEvents(string calendarId, int maxResults)
        {
            var events = _calendarService.GetEvents(calendarId, maxResults);
            return Ok(events);
        }

    }
}
