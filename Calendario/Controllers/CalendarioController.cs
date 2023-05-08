using Calendario.Servico;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("Eventos")]
        [SwaggerUI(SwaggerUIType.DatePicker, "dataInicio", "Data de início", "Selecione a data de início", "dataFinal", "Data final", "Selecione a data final")]
        public async Task<IActionResult> GetTodosEventos(DateTime dataInicio, DateTime dataFinal)
        {
            var events = _calendarService.GetEvents(dataInicio, dataFinal);

            return Ok(events);
        }

    }
}
