using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PublicWorks.API.Helpers
{
    public class UserHelper: IUserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetLoggedInUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity.IsAuthenticated)
                return null;

            var claim = user.FindFirst("UserId") ?? user.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int userId))
                return userId;

            return null;
        }

        public string? GetLoggedInUserRole()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
