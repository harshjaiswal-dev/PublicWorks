namespace Business.DTOs
{
    public class MessageDto
    {
        public int ID { get; set; }
        public int IssueId { get; set; }
        public int SendBy { get; set; }
        public int SendTo { get; set; }      
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;  
        public DateTime SendDate { get; set; }
    }
}