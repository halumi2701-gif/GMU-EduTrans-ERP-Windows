namespace GMUEduTrans.Desktop.Models;

public sealed class AppState
{
    public int TargetPax { get; set; } = 400;
    public List<BookingRecord> Bookings { get; set; } = new();
    public List<LeadRecord> Leads { get; set; } = new();
    public List<OperationRecord> Operations { get; set; } = new();
    public List<FinanceRecord> Finances { get; set; } = new();
    public List<StaffRecord> Staff { get; set; } = new();
}

public sealed class BookingRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Date { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");
    public string School { get; set; } = "";
    public string Program { get; set; } = "";
    public int Participants { get; set; }
    public string Status { get; set; } = "Draft";
}

public sealed class LeadRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string School { get; set; } = "";
    public string Contact { get; set; } = "";
    public string Stage { get; set; } = "Prospek";
    public decimal Potential { get; set; }
}

public sealed class OperationRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Date { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");
    public string Activity { get; set; } = "";
    public string PIC { get; set; } = "";
    public string Status { get; set; } = "Belum";
}

public sealed class FinanceRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Date { get; set; } = DateTime.Today.ToString("yyyy-MM-dd");
    public string Description { get; set; } = "";
    public string Type { get; set; } = "Pemasukan";
    public decimal Amount { get; set; }
}

public sealed class StaffRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = "";
    public string Role { get; set; } = "";
    public string Scheme { get; set; } = "";
    public string Status { get; set; } = "Aktif";
}
