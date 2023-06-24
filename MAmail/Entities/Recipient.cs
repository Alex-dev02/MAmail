namespace MAmail.Entities
{
    public class Recipient
    {

        public int Id { get; set; }
        public bool IsRead { get; set; }
        public bool IsArchived { get; set; }
        public int RecipentId { get; set; }
        public User? Recipent { get; set; }
        public int EmailId { get; set; }
        public Email? Email { get; set; }
    }
}
