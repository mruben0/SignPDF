using System;

namespace SignPDF.Services.Abstract
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        string CurrentPageKey { get; }

        void RegisterView(string key, Uri pageType);

        void GoBack();

        void Navigate(string pageKey, bool addHistory = true);

    }
}