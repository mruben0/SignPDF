using SignPDF.ViewModels;
using System.Windows.Controls;

namespace SignPDF.Views
{
    public partial class SettingsView : Page
    {
        public SettingsView()
        {
            InitializeComponent();
            Loaded += SettingsView_Loaded;
        }

        private void SettingsView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel vm)
            {
                vm.Initialize();
            }
        }
    }
}