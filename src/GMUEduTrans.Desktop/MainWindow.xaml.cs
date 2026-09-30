using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GMUEduTrans.Desktop;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Nav.SelectedItem = Nav.MenuItems[0];
    }

    private void Nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        // Native module navigation will be wired to dedicated Pages/ViewModels.
    }
}