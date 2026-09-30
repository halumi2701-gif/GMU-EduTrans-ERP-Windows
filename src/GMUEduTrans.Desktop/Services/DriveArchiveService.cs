using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace GMUEduTrans.Desktop.Services;

public sealed record DriveArchiveFolder(string FolderId, string FolderUrl, string FolderName);

public sealed class DriveArchiveService
{
    private const string SupabaseUrl = "https://gtgnwasijweewmaubvyg.supabase.co";
    private const string PublishableKey = "sb_publishable_cbTtSEhcXsHKDdldocSw3Q_bTcfXtaW";
    private readonly HttpClient _http = new() { Timeout = TimeSpan.FromSeconds(30) };

    public async Task<bool> HealthAsync(CancellationToken cancellationToken = default)
    {
        var response = await InvokeAsync(new { action = "health" }, cancellationToken);
        return response.RootElement.TryGetProperty("ok", out var ok) && ok.GetBoolean();
    }

    public async Task<DriveArchiveFolder> EnsureOrderFolderAsync(
        string bookingNo,
        string customerName,
        string activityDate,
        CancellationToken cancellationToken = default)
    {
        using var response = await InvokeAsync(new
        {
            action = "ensure_order_folder",
            entity_id = bookingNo,
            booking_code = bookingNo,
            customer_name = customerName,
            activity_date = activityDate
        }, cancellationToken);

        var root = response.RootElement;
        if (!root.TryGetProperty("ok", out var ok) || !ok.GetBoolean())
            throw new InvalidOperationException(root.TryGetProperty("error", out var error)
                ? error.GetString()
                : "Folder Google Drive gagal dibuat.");

        var folder = root.GetProperty("folder");
        return new DriveArchiveFolder(
            folder.GetProperty("drive_folder_id").GetString() ?? "",
            folder.GetProperty("drive_folder_url").GetString() ?? "",
            folder.GetProperty("folder_name").GetString() ?? "");
    }

    private async Task<JsonDocument> InvokeAsync(object payload, CancellationToken cancellationToken)
    {
        var accessToken = SessionService.Current.AccessToken;
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new InvalidOperationException("Sesi ERP belum aktif.");

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{SupabaseUrl}/functions/v1/gmu-drive-archive");
        request.Headers.Add("apikey", PublishableKey);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Content = JsonContent.Create(payload);

        using var response = await _http.SendAsync(request, cancellationToken);
        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var message = json;
            try
            {
                using var parsed = JsonDocument.Parse(json);
                if (parsed.RootElement.TryGetProperty("error", out var error))
                    message = error.GetString() ?? json;
            }
            catch { }
            throw new InvalidOperationException(message);
        }

        return JsonDocument.Parse(json);
    }
}
