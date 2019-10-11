using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using SignPDF.Services.Abstract;
using System.Windows.Input;

namespace SignPDF.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        private readonly IPDFSigningService _signingService;
        private string _inputPath;
        private string _outPath;
        private string _imagePath;

        public MainViewModel(IPDFSigningService signingService)
        {
            if (IsInDesignMode)
            {
                return;
            }

            _signingService = signingService ?? throw new System.ArgumentNullException(nameof(signingService));
        }

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