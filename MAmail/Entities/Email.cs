namespace MAmail.Entities
{
    public class Email
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime TimeStamp { get; set; }
        public int SenderId { get; set; }
        public User? Sender { get; set; }

    }
}
