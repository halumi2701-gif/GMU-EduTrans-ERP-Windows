namespace GMUEduTrans.Desktop.Services;

public static class RoleAccessService
{
    public static bool CanAccess(UserRole role, string module) => role switch
    {
        UserRole.Owner => true,
        UserRole.Manager => module is not "settings",
        UserRole.Sales => module is "dashboard" or "booking" or "sales" or "program",
        UserRole.Admin => module is "dashboard" or "booking" or "program" or "master",
        UserRole.Finance => module is "dashboard" or "finance" or "reports",
        UserRole.TourLeader => module is "dashboard" or "operations",
        UserRole.Operations => module is "dashboard" or "operations" or "booking",
        _ => false
    };
}