namespace GMUEduTrans.Desktop.Services;

public enum UserRole { Owner, Manager, Sales, Admin, Finance, TourLeader, Operations }

public sealed class SessionService
{
    public static SessionService Current { get; } = new();
    public string DisplayName { get; private set; } = "GMU EduTrans";
    public UserRole Role { get; private set; } = UserRole.Owner;
    public string? AccessToken { get; private set; }

    public void SetSession(string displayName, UserRole role, string accessToken)
    {
        DisplayName = displayName;
        Role = role;
        AccessToken = accessToken;
    }

    public void Clear()
    {
        DisplayName = "GMU EduTrans";
        Role = UserRole.Owner;
        AccessToken = null;
    }
}