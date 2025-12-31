using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace GetControlInViewModel.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private AxWMPLib.AxWindowsMediaPlayer mediaPlayer;

        public RelayCommand<AxWMPLib.AxWindowsMediaPlayer> OnLoadedCommand { get; private set; }
        
        public MainViewModel()
        {
            OnLoadedCommand = new RelayCommand<AxWMPLib.AxWindowsMediaPlayer>(OnLoaded);
        }

        private void OnLoaded(AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer)
        {
            this.mediaPlayer = axWindowsMediaPlayer;
        }
    }
}