using Calendario.Modelos.Parametros;
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

        [HttpGet]
        public async Task<IActionResult> GetAgendas()
        {
            var agendas = _calendarService.ListCalendars();
            return Ok(agendas);
        }

        [HttpGet("{calendarId}")]
        public async Task<IActionResult> GetCalendar(string calendarId)
        {
            var calendar = await _calendarService.GetCalendar(calendarId);
            return Ok(calendar);
        }

        [HttpGet("{calendarId}/events")]
        public async Task<IActionResult> GetCalendarEvents(string calendarId, int maxResults)
        {
            var events = _calendarService.GetEvents(calendarId, maxResults);
            return Ok(events);
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetTodosEventos([FromQuery] EventsDateParameters date)
        {
            var events = _calendarService.GetEvents(date.DataInicio, date.DataFinal);

            return Ok(events);
        }

    }
}
