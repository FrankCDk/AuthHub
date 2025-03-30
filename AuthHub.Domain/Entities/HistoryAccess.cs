namespace AuthHub.Domain.Entities
{
    public class HistoryAccess
    {
        public int IdUser { get; set; } = 0;
        public DateTime FechaAcceso { get; set; }
        public string IpUser { get; set; } = string.Empty;
        public bool Exito {  get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
