namespace Calendario.Modelos.Parametros;

public class CalendarEventsParameters
{
    public string CalendarId { get; set; }
    public bool AlwaysIncludeEmail { get; set; }
    public string[] EventTypes { get; set; }
    public string ICalUID { get; set; }
    public int MaxAttendees { get; set; }
    public int MaxResults { get; set; }
    public string OrderBy { get; set; }
    public string PageToken { get; set; }
    public string[] PrivateExtendedProperty { get; set; }
    public string Q { get; set; }
    public string[] SharedExtendedProperty { get; set; }
    public bool ShowDeleted { get; set; }
    public bool ShowHiddenInvitations { get; set; }
    public bool SingleEvents { get; set; }
    public string SyncToken { get; set; }
}
