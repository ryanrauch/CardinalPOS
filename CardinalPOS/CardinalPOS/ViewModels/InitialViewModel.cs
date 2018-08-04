using CardinalPOS.Services.Interfaces;
using CardinalPOS.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CardinalPOS.ViewModels
{
    public class InitialViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public InitialViewModel(
            INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand MainPageViewCommand => new Command(() => _navigationService.NavigateToMain());

        public ICommand TicketAccessControlViewCommand => new Command(() => _navigationService.NavigateToTicketAccess());

        public override Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }
    }
}
