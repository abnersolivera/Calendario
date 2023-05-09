using Calendario.Modelos.Dto;
using Calendario.Modelos.Model;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Globalization;
using Calendar = Google.Apis.Calendar.v3.Data.Calendar;

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

    public DateModel<EventsDto> GetEvents(DateTime dataInicio, DateTime dataFinal)
    {
        var calendarios = ListCalendars();

        var listBlack = new List<string>() 
        { 
            ""
        };


        var events = new List<Event>();
        var eventsDto = new List<EventsDto>();

        foreach (var item in calendarios)
        {
            var id = item.Id;
            if (!listBlack.Contains(id))
            {
                EventsResource.ListRequest request = _calendarService.Events.List(id);
                request.TimeMin = dataInicio;
                request.TimeMax = dataFinal;
                var result = request.Execute();
                events.AddRange(result.Items);
            }            

        }

        foreach (var @event in events)
        {
            var client = @event.Summary;
            var service = @event.Organizer.DisplayName;
            var dateService = @event.Start.DateTime; 
            eventsDto.Add(new EventsDto() 
            { 
                Client = client,
                Service = service,
                DateService = dateService,
                Price = 45.00
            });

        }

        return new DateModel<EventsDto>
        {
            Data = eventsDto,
            Details = new DetailsModel() 
            {
                TotalService = eventsDto.Select(x => x.Service).Count(),
                TotalPrice = eventsDto.Select(x => x.Price).Sum()
            }
        };
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

