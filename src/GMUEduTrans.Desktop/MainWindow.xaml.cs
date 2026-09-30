using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GMUEduTrans.Desktop;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var title = new TextBlock
        {
            Text = "GMU EduTrans ERP",
            FontSize = 28,
            FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
            Margin = new Thickness(32, 28, 32, 8)
        };

        var subtitle = new TextBlock
        {
            Text = "Desktop Native • Windows",
            FontSize = 15,
            Opacity = 0.65,
            Margin = new Thickness(32, 0, 32, 24)
        };

        var status = new TextBlock
        {
            Text = "Aplikasi berhasil dijalankan. Modul ERP sedang dimuat.",
            FontSize = 16,
            Margin = new Thickness(32, 12, 32, 12)
        };

        var panel = new StackPanel();
        panel.Children.Add(title);
        panel.Children.Add(subtitle);
        panel.Children.Add(status);
        RootGrid.Children.Add(panel);
    }
}