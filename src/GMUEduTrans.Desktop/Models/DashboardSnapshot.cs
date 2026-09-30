namespace GMUEduTrans.Desktop.Models;

public sealed record DashboardSnapshot(
    int TargetPax,
    int TotalPeserta,
    int BookingAktif,
    decimal Omzet,
    decimal LabaBersih,
    decimal SaldoKas,
    decimal Kewajiban);