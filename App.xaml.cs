using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp1_MeinHundNanga.ViewModels;
using WpfApp1_MeinHundNanga.Views;

namespace WpfApp1_MeinHundNanga;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public AuftragsstarterWindow aWindow { get; set; }
    public AuftragsstarterViewModel aViewModel { get; set; } = new AuftragsstarterViewModel();

    public MainWindowViewModel mainWindowViewModel { get; set; } = new MainWindowViewModel();


    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        aWindow = new AuftragsstarterWindow();
        aWindow.Show();
    }
}
