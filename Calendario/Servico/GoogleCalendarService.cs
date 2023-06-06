using Calendario.Modelos.Dto;
using Calendario.Modelos.Entidades;
using Calendario.Modelos.Model;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.RegularExpressions;
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
            "abnersanto2014@gmail.com",
            "addressbook#contacts@group.v.calendar.google.com",
            "39968cf28cb4d070d0fdfa9d68bffb81bc4591e3dd76355ca50d283736e3795a@group.calendar.google.com",
            "c_hf2mevms7d4nrujdb28mu33qgc@group.calendar.google.com"
        };


        var events = new List<Event>();
        var eventsDto = new List<EventsDto>();
        var servicos = new Dictionary<string, ServicosEntidades>();
        CultureInfo cultura = new CultureInfo("pt-BR");
        var price = 0.00m;

        foreach (var item in calendarios)
        {
            var id = item.Id;
            if (!listBlack.Contains(id))
            {
                var values = item.Description;
                var value = @"ValorAnterio:\s*([\d,]+).*ValorAtual:\s*([\d,]+)";
                var match = values is null ? null : Regex.Match(values, value);

                string valorAnteriorString = match.Groups[1].Value;
                string valorAtualString = match.Groups[2].Value;

                int indiceVirgula = valorAnteriorString.Select((c, i) => new { Character = c, Index = i })
                                    .LastOrDefault(x => x.Character == ',')?.Index ?? -1;

                valorAnteriorString = indiceVirgula != 1 ? valorAnteriorString.Remove(indiceVirgula,1) : valorAnteriorString; 

                var valorAnterior = match is null ? 0.00m : decimal.Parse(valorAnteriorString, new CultureInfo("pt-BR"));
                var valorAtual = match is null ? 0.00m : decimal.Parse(valorAtualString, new CultureInfo("pt-BR"));

                servicos.Add(item.Summary, new ServicosEntidades { PrecoAtual = valorAtual, PrecoAnterior = valorAnterior });

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
            if (servicos.TryGetValue(service, out ServicosEntidades servico))
            {                
                price = servico.PrecoAtual;
            }
            else
            {
                var erro = "";
            }
            
            eventsDto.Add(new EventsDto()
            {
                Client = client,
                Service = service,
                DateService = dateService,
                Price = price
            });

        }

        // Ordena a lista de eventos por data, nome e final do serviço
        var eventosOrdenados = eventsDto.OrderBy(e => e.DateService)
                                        .ThenBy(e => e.Service)
                                        .ThenBy(e => e.Client)
                                        .ThenBy(e => e.Price)
                                        .ToList();

        return new DateModel<EventsDto>
        {
            Data = eventosOrdenados,
            Details = new DetailsModel()
            {
                DesignSimples = eventsDto.Count(x => x.Service.Equals("Design simples")),
                DesignComHenna = eventsDto.Count(x => x.Service.Equals("Design com henna")),
                DesignComHennaEBuco = eventsDto.Count(x => x.Service.Equals("Design com henna e buço")),
                Buco = eventsDto.Count(x => x.Service.Equals("Buço")),
                DesignSimplesEBuco = eventsDto.Count(x => x.Service.Equals("Design simples e buço")),
                EpilacaoFacial = eventsDto.Count(x => x.Service.Equals("Epilação facial")),
                LashLifting = eventsDto.Count(x => x.Service.Equals("Lash Lifting")),
                Microblading = eventsDto.Count(x => x.Service.Equals("Microblading")),
                RetoqueMicroblading = eventsDto.Count(x => x.Service.Equals("Retoque microblading")),
                Shadow = eventsDto.Count(x => x.Service.Equals("Shadow")),
                RetoqueShadow = eventsDto.Count(x => x.Service.Equals("Retoque shadow")),
                ShadowLine = eventsDto.Count(x => x.Service.Equals("Shadow Line")),
                RetoqueShadowLine = eventsDto.Count(x => x.Service.Equals("Retoque shadow line")),
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

