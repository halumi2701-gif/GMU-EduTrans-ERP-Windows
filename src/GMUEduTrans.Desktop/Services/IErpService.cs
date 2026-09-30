using GMUEduTrans.Desktop.Models;

namespace GMUEduTrans.Desktop.Services;

public interface IErpService
{
    Task<DashboardSnapshot> GetDashboardAsync(CancellationToken cancellationToken = default);
}