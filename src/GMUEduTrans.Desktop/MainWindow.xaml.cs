using GMUEduTrans.Desktop.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GMUEduTrans.Desktop;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Nav.SelectedItem = Nav.MenuItems[0];
        ContentFrame.Navigate(typeof(DashboardPage));
    }

    private void Nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            NavigateModule("Pengaturan");
            return;
        }

        if (args.SelectedItemContainer is not NavigationViewItem item) return;
        var tag = item.Tag?.ToString();
        if (tag == "dashboard")
        {
            if (ContentFrame.CurrentSourcePageType != typeof(DashboardPage))
                ContentFrame.Navigate(typeof(DashboardPage));
            return;
        }

        NavigateModule(item.Content?.ToString() ?? "Modul");
    }

    private void NavigateModule(string title)
    {
        ContentFrame.Navigate(typeof(ModulePage));
        if (ContentFrame.Content is ModulePage page)
            page.Tag = title;
    }
}