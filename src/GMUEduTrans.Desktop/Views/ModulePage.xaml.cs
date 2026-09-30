using Microsoft.UI.Xaml.Controls;

namespace GMUEduTrans.Desktop.Views;

public sealed partial class ModulePage : Page
{
    public ModulePage()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            var title = Tag?.ToString() ?? "Modul";
            TitleText.Text = title;
            SubtitleText.Text = $"Workspace {title} GMU EduTrans.";
        };
    }
}