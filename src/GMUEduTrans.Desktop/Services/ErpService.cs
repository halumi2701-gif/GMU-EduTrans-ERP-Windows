using System.Net.Http.Json;
using GMUEduTrans.Desktop.Models;

namespace GMUEduTrans.Desktop.Services;

public sealed class ErpService : IErpService
{
    private readonly HttpClient _http = new()
    {
        Timeout = TimeSpan.FromSeconds(20)
    };

    public async Task<DashboardSnapshot> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var apiBase = Environment.GetEnvironmentVariable("GMU_EDUTRANS_API_BASE");
        if (string.IsNullOrWhiteSpace(apiBase))
            return new DashboardSnapshot(400, 0, 0, 0, 0, 0, 0);

        _http.BaseAddress ??= new Uri(apiBase.TrimEnd('/') + "/");
        return await _http.GetFromJsonAsync<DashboardSnapshot>("desktop/dashboard", cancellationToken)
               ?? new DashboardSnapshot(400, 0, 0, 0, 0, 0, 0);
    }
}