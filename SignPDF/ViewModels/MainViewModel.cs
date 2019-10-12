using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SignPDF.Services.Abstract;
using SignPDF.Views;
using System;
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
            GoBackCommand.RaiseCanExecuteChanged();
        });

        public RelayCommand GoBackCommand => new RelayCommand(() =>
        {
            _navigationService.GoBack();
        }, CanGoBack);

        private bool CanGoBack()
        {
            return _navigationService.CanGoBack;
        }
    }
}