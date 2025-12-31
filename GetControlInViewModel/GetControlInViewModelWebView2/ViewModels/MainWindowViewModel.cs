using Microsoft.Web.WebView2.Wpf;
using Prism.Commands;

namespace GetControlInViewModelWebView2.ViewModels
{
    public class MainWindowViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private WebView2 webView;

        public DelegateCommand<WebView2> OnLoadedCommand { get; private set; }

        public MainWindowViewModel()
        {
            OnLoadedCommand = new DelegateCommand<WebView2>(OnLoaded);
        }

        private void OnLoaded(WebView2 webView2)
        {
            this.webView = webView2;
        }
    }
}