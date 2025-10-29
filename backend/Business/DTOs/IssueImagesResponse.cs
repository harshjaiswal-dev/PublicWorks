using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class IssueImagesResponse
    {
        public string Message { get; set; } = string.Empty;
        public IEnumerable<IssueImageDto>? Data { get; set; }
    }
}
