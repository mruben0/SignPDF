using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SignPDF.Services.Abstract;
using SignPDF.Views;
using System.Diagnostics;
using System.Windows.Input;

namespace SignPDF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
                if (IsInDesignMode)
                {
                    return;
                }

            _navigationService = navigationService ?? throw new System.ArgumentNullException(nameof(navigationService));
        }

        internal void Initialize()
        {
            _navigationService.Navigate(nameof(HomeView));
        }

        public ICommand BrowseGithubCommand => new RelayCommand(() =>
        {
            Process.Start("https://github.com/mruben0/SignPDF");
        });

        public ICommand NavigateToCommand => new RelayCommand<string>((navigationTag) =>
        {
            _navigationService.Navigate(navigationTag);
            RaisePropertyChanged(nameof(CanGoBack));
        });

        public ICommand GoBackCommand => new RelayCommand(() =>
        {
            _navigationService.GoBack();
            RaisePropertyChanged(nameof(CanGoBack));
        });

        public bool CanGoBack => _navigationService.CanGoBack;
    }
}