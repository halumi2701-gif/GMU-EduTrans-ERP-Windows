using System.Text.Json;
using GMUEduTrans.Desktop.Models;

namespace GMUEduTrans.Desktop.Services;

public sealed class LocalDataStore
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _json = new() { WriteIndented = true };

    public LocalDataStore()
    {
        var dir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "GMU EduTrans ERP");
        Directory.CreateDirectory(dir);
        _filePath = Path.Combine(dir, "data.json");
    }

    public AppState Load()
    {
        try
        {
            if (!File.Exists(_filePath))
                return new AppState();

            var text = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<AppState>(text, _json) ?? new AppState();
        }
        catch
        {
            return new AppState();
        }
    }

    public void Save(AppState state)
    {
        var temp = _filePath + ".tmp";
        var json = JsonSerializer.Serialize(state, _json);
        File.WriteAllText(temp, json);
        File.Move(temp, _filePath, true);
    }

    public string FilePath => _filePath;
}
