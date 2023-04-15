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
}

