using Calendario.Modelos.Dto;
using Calendario.Modelos.Model;
using Google.Apis.Calendar.v3.Data;

namespace Calendario.Modelos.Interface
{
    public interface IGoogleCalendarService
    {
        IList<Event> GetEvents(string calendarId, int maxResults);
        DateModel<EventsDto> GetEvents(DateTime dataInicio, DateTime dataFinal);
        IList<CalendarListEntry> ListCalendars();
        Task<Calendar> GetCalendar(string calendarId);
    }
}
