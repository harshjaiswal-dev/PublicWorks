namespace Business.DTOs
{
    public class GoogleTokenResponseDto
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; } // may be null if not granted
        public string scope { get; set; }
        public string token_type { get; set; }
        public string id_token { get; set; }
    }
}

