using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1_MeinHundNanga.Views
{
    /// <summary>
    /// Interaction logic for AuftragsstarterWindow.xaml
    /// </summary>
    public partial class AuftragsstarterWindow : Window
    {
        public static readonly App CurrentApp = (Application.Current as App)!;
        public AuftragsstarterWindow()
        {
            InitializeComponent();

            DataContext = CurrentApp.aViewModel;
        }
    }
}
