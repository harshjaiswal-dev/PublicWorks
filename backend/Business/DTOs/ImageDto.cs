namespace Business.DTOs
{
    public class ImageDto
    {
        public int ID { get; set; }
        public int IssueId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}