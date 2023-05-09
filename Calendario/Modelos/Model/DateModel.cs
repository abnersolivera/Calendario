namespace Calendario.Modelos.Model
{
    public class DateModel <T>
    {
        public IEnumerable<T> Data { get; set; }
        public DetailsModel Details { get; set; }
    }
}
