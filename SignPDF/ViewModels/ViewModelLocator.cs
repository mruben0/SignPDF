using System;
using System.Collections.Generic;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using SignPDF.Services;
using SignPDF.Services.Abstract;
using SignPDF.Views;

namespace SignPDF.ViewModels
{   
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsView>();
            SimpleIoc.Default.Register<HomeView>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SetupNavigation();
            RegisterServices();        
        }

        private void RegisterServices()
        {
            SimpleIoc.Default.Register<IPDFSigningService, PDFSigningService>();
        }

        private static void SetupNavigation()
        {
            var navigationService = new NavigationService();
            navigationService.RegisterView(nameof(SettingsView), new Uri("Views/SettingsView.xaml", UriKind.Relative));
            navigationService.RegisterView(nameof(HomeView), new Uri("Views/HomeView.xaml", UriKind.Relative));         
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
        }

        public MainViewModel MainVM => ServiceLocator.Current.GetInstance<MainViewModel>();
        public SettingsViewModel SettingsVM => ServiceLocator.Current.GetInstance<SettingsViewModel>();
        public HomeViewModel HomeVM => ServiceLocator.Current.GetInstance<HomeViewModel>();  
    }
}