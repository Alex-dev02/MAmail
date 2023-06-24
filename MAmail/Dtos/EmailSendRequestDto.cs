namespace MAmail.Dtos
{
    public class EmailSendRequestDto
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<string> RecipientsEmails { get; set; }
    }
}
