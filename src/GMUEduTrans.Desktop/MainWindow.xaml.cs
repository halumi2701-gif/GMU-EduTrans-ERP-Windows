using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GMUEduTrans.Desktop;

public sealed class MainWindow : Window
{
    public MainWindow()
    {
        Title = "GMU EduTrans ERP";

        var root = new Grid();
        var panel = new StackPanel
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top
        };

        panel.Children.Add(new TextBlock
        {
            Text = "GMU EduTrans ERP",
            FontSize = 28,
            FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
            Margin = new Thickness(32, 28, 32, 8)
        });
        panel.Children.Add(new TextBlock
        {
            Text = "Desktop Native • Windows",
            FontSize = 15,
            Opacity = 0.65,
            Margin = new Thickness(32, 0, 32, 24)
        });
        panel.Children.Add(new TextBlock
        {
            Text = "Aplikasi berhasil dijalankan.",
            FontSize = 16,
            Margin = new Thickness(32, 12, 32, 12)
        });

        root.Children.Add(panel);
        Content = root;
    }
}