using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

namespace Calendario.Servico;

public class GoogleCalendarService
{
    public readonly CalendarService _calendarService;

    public GoogleCalendarService(string applicationName, GoogleCalendarAuthorization calendarAuthorization)
    {
        var userCredential = calendarAuthorization.GetUserCredential().Result;
        _calendarService = new CalendarService(new BaseClientService.Initializer()
        {
            ApplicationName = applicationName,
            HttpClientInitializer = userCredential
        });
    }

    public IList<Event> GetEvents(string calendarId, int maxResults)
    {
        EventsResource.ListRequest request = _calendarService.Events.List(calendarId);
        request.MaxResults = maxResults;

        Events events = request.Execute();
        IList<Event> items = events.Items;
        return items;
    }

    public IList<Event> GetEvents(DateTime dataInicio, DateTime dataFinal)
    {
        var calendarios = ListCalendars();

        var events = new List<Event>();

        foreach (var item in calendarios)
        {
            var id = item.Id;
            EventsResource.ListRequest request = _calendarService.Events.List(id);
            request.TimeMin = dataInicio;
            request.TimeMax = dataFinal;
            var result = request.Execute();
            events.AddRange(result.Items);
        }
        
        return events;
    }

    public IList<CalendarListEntry> ListCalendars()
    {
        var request = _calendarService.CalendarList.List();
        var calendarList = request.Execute();
        return calendarList.Items;

    }

    public async Task<Calendar> GetCalendar(string calendarId)
    {
        return await _calendarService.Calendars.Get(calendarId).ExecuteAsync();
    }

}

