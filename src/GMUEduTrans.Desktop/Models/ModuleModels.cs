namespace GMUEduTrans.Desktop.Models;

public sealed record BookingItem(string Date, string School, string Program, int Participants, string Status);
public sealed record LeadItem(string School, string Contact, string Stage, decimal Potential);
public sealed record OperationItem(string Time, string Activity, string Status);
public sealed record FinanceItem(string Date, string Description, string Type, decimal Amount);
public sealed record StaffItem(string Name, string Role, string Scheme, string Status);