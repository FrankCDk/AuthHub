namespace AuthHub.Domain.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string? Type {  get; set; }
        public string? Ip {  get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime Date { get; set; }
    }
}
