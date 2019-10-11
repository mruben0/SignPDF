using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using SignPDF.Services.Abstract;
using SignPDF.Views;
using System.Diagnostics;
using System.Windows.Input;

namespace SignPDF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPDFSigningService _signingService;
        public readonly INavigationService _navigationService;
        private string _inputPath;
        private string _outPath;
        private string _imagePath;

        public MainViewModel(IPDFSigningService signingService, INavigationService navigationService)
        {
            if (IsInDesignMode)
            {
                return;
            }

            _signingService = signingService ?? throw new System.ArgumentNullException(nameof(signingService));
            this._navigationService = navigationService ?? throw new System.ArgumentNullException(nameof(navigationService));
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

        public ICommand GetPathCommand => new RelayCommand<object>((o) =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            switch (o)
            {
                case "file":
                    openFileDialog.Filter = "Pdf Files|*.pdf";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        InputPath = openFileDialog.FileName;
                    }
                    break;

                case "image":
                    openFileDialog.Filter = "Image Files(*.bmp; *.jpg; *.jpeg,*.png) | *.BMP;*.JPG;*.JPEG;*.PNG";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        ImagePath = openFileDialog.FileName;
                    }
                    break;

                default:
                    break;
            }
        });

        public ICommand SignCommand => new RelayCommand(() =>
        {
            SaveFileDialog sfdl = new SaveFileDialog();
            sfdl.Filter = "Pdf Files|*.pdf";
            if (sfdl.ShowDialog() == true)
            {
                OutPath = sfdl.FileName;
                _signingService.Sign(InputPath, OutPath, ImagePath, true);
            }
        });

        public string InputPath
        {
            get { return _inputPath; }
            set
            {
                _inputPath = value;
                RaisePropertyChanged();
            }
        }

        public string OutPath
        {
            get { return _outPath; }
            set
            {
                _outPath = value;
                RaisePropertyChanged();
            }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value;
                RaisePropertyChanged();
            }
        }
    }
}