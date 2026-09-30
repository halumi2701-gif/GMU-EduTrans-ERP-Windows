using GMUEduTrans.Desktop.Services;
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
        ApplyRoleVisibility();
    }

    private void ApplyRoleVisibility()
    {
        foreach (var raw in Nav.MenuItems)
            if (raw is NavigationViewItem item && item.Tag is string tag)
                item.Visibility = RoleAccessService.CanAccess(SessionService.Current.Role, tag)
                    ? Visibility.Visible : Visibility.Collapsed;
    }

    private void Nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.IsSettingsSelected)
        {
            if (RoleAccessService.CanAccess(SessionService.Current.Role, "settings"))
                Navigate(typeof(ModulePage), "Pengaturan");
            return;
        }

        if (args.SelectedItemContainer is not NavigationViewItem item) return;
        var tag = item.Tag?.ToString() ?? "";
        if (!RoleAccessService.CanAccess(SessionService.Current.Role, tag)) return;

        switch (tag)
        {
            case "dashboard": Navigate(typeof(DashboardPage)); break;
            case "booking": Navigate(typeof(BookingPage)); break;
            case "sales": Navigate(typeof(SalesPage)); break;
            case "operations": Navigate(typeof(OperationsPage)); break;
            case "finance": Navigate(typeof(FinancePage)); break;
            case "hr": Navigate(typeof(HrPage)); break;
            case "reports": Navigate(typeof(ReportsPage)); break;
            default: Navigate(typeof(ModulePage), item.Content?.ToString()); break;
        }
    }

    private void Navigate(Type pageType, string? title = null)
    {
        if (ContentFrame.CurrentSourcePageType == pageType && pageType != typeof(ModulePage)) return;
        ContentFrame.Navigate(pageType);
        if (title is not null && ContentFrame.Content is ModulePage page) page.Tag = title;
    }
}