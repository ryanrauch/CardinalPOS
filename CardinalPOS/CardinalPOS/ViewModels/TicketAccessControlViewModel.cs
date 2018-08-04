using CardinalPOS.Services.Interfaces;
using CardinalPOS.ViewModels.Base;
using CardinalPOSLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardinalPOS.ViewModels
{
    public class TicketAccessControlViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly INFCService _nfcService;

        public TicketAccessControlViewModel(
            INFCService nfcService,
            INavigationService navigationService)
        {
            _nfcService = nfcService;
            _navigationService = navigationService;
            _nfcService.TagDetected += _nfcService_TagDetected;
        }

        private void _nfcService_TagDetected(INfcDefTag tag)
        {
            string msg = string.Empty;
            int i = 0;
            foreach(var t in tag.Records)
            {
                msg += string.Format("{0}:{1}", ++i, t.Payload);
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
            if(!await _nfcService.IsAvailableAsync())
            {
                Message = "NFC Reader is not available.";
                //return;
            }
            if(!await _nfcService.IsEnabledAsync())
            {
                Message = "NFC Reader is not enabled. Please turn it on in the settings.";
                //return;
            }

            await _nfcService.StartListeningAsync();
        }
    }
}
