using System.Globalization;
using GMUEduTrans.Desktop.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GMUEduTrans.Desktop.Views;

public sealed partial class DashboardPage : Page
{
    private readonly IErpService _erp = new ErpService();

    public DashboardPage()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            var d = await _erp.GetDashboardAsync();
            var id = CultureInfo.GetCultureInfo("id-ID");
            TargetPaxText.Text = d.TargetPax.ToString("N0", id);
            PesertaText.Text = d.TotalPeserta.ToString("N0", id);
            OmzetText.Text = d.Omzet.ToString("C0", id);
            LabaText.Text = d.LabaBersih.ToString("C0", id);
            TargetProgress.Maximum = Math.Max(1, d.TargetPax);
            TargetProgress.Value = d.TotalPeserta;
            TargetCaption.Text = $"{d.TotalPeserta:N0} dari target {d.TargetPax:N0} pax bulan ini";
        }
        catch
        {
            TargetCaption.Text = "Data ERP belum dapat disinkronkan. Coba lagi setelah koneksi API aktif.";
        }
    }
}