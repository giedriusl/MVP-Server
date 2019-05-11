namespace MVP.Entities.Dtos.Emails
{
    public class EmailMessageDto
    {
        public string ToAddress { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
