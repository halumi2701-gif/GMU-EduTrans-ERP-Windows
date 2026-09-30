using Microsoft.UI.Xaml;

namespace GMUEduTrans.Desktop;

public partial class App : Application
{
    private Window? _window;

    public App()
    {
        InitializeComponent();
        UnhandledException += OnUnhandledException;
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        try
        {
            _window = new MainWindow();
            _window.Activate();
        }
        catch (Exception ex)
        {
            ShowStartupError(ex);
        }
    }

    private void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        ShowStartupError(e.Exception);
        e.Handled = true;
    }

    private static void ShowStartupError(Exception ex)
    {
        try
        {
            var message = "GMU EduTrans ERP gagal dibuka.\n\n" + ex;
            System.IO.File.WriteAllText(
                System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    "GMU-EduTrans-Startup-Error.txt"),
                message);

            _ = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "notepad.exe",
                    Arguments = "\"GMU-EduTrans-Startup-Error.txt\"",
                    WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    UseShellExecute = true
                }
            }.Start();
        }
        catch { }
    }
}