namespace Business.Service.Settings
{
    /// <summary>
    /// Represents configuration settings for JSON Web Tokens (JWT).
    /// This class is strongly typed and can be bound from appsettings.json.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Secret key used to sign JWT tokens.
        /// Must be kept secure and should be at least 256 bits.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Issuer of the token. Typically the name of your application or service.
        /// Used to validate the "iss" claim when the token is received.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Audience of the token. Usually identifies the recipients that the token is intended for.
        /// Used to validate the "aud" claim when the token is received.
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Lifetime of the access token in minutes.
        /// Short-lived token reduces risk if stolen.
        /// Example: 30 means the token expires 30 minutes after issuance.
        /// </summary>
        public int AccessTokenExpiryMinutes { get; set; }

        /// <summary>
        /// Lifetime of the refresh token in days.
        /// Long-lived token used to request new access tokens without logging in.
        /// Example: 30 means the refresh token expires after 30 days.
        /// </summary>
        public int RefreshTokenExpiryDays { get; set; }
    }
}
