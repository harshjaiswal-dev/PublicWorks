namespace PublicWorks.API.Helpers
{
    public interface IUserHelper
    {
        int? GetLoggedInUserId();
          string? GetLoggedInUserRole();
    }
}
