namespace MAmail.Dtos
{
    public class EmailHeaderDto
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
