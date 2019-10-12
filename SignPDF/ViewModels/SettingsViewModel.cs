using GalaSoft.MvvmLight;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SignPDF.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            
        }

        public void Initialize()
        {
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Red"),
                                        ThemeManager.GetAppTheme("BaseDark"));
        }
    }
}
