namespace Calendario.Modelos.Dto
{
    public class EventsDto
    {
        public string Client { get; set; }
        public string Service { get; set; }
        public DateTime? DateService { get; set; } = null;
        public decimal Price { get; set; }

    }
}
