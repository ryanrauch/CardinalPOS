using CardinalPOS.Services.Interfaces;
using CardinalPOS.ViewModels.Base;
using Plugin.Nfc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardinalPOS.ViewModels
{
    public class TicketAccessControlViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public TicketAccessControlViewModel(
            INavigationService navigationService)
        {
            _navigationService = navigationService;
            CrossNfc.Current.TagDetected += Current_NFCTagDetected;
        }

        private void Current_NFCTagDetected(Plugin.Nfc.Abstractions.INfcDefTag tag)
        {
            string msg = string.Empty;
            int i = 0;
            foreach (var t in tag.Records)
            {
                ++i;
                msg += string.Format("{0}:{1}", ++i,
                                                t.Payload.ToString());
            }
            Message = msg;
        }

        private string _message { get; set; }
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        public override async Task OnAppearingAsync()
        {
            if(!await CrossNfc.Current.IsAvailableAsync())
            {
                Message = "NFC Reader is not available.";
                //return;
            }
            if(!await CrossNfc.Current.IsEnabledAsync())
            {
                Message = "NFC Reader is not enabled. Please turn it on in the settings.";
                //return;
            }

            await CrossNfc.Current.StartListeningAsync();
        }
    }
}
