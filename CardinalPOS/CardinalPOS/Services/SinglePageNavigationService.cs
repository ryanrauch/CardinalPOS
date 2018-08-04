using CardinalPOS.Services.Interfaces;
using CardinalPOS.ViewModels.Base;
using CardinalPOS.Views.ContentPages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CardinalPOS.Services
{
    public class SinglePageNavigationService : INavigationService
    {
        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public Task NavigatePopAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigatePushAsync<T>(T page) where T : Page
        {
            CurrentApplication.MainPage = page;
            return Task.CompletedTask;
        }

        public Task NavigatePushAsync<T>(T page, object param) where T : Page
        {
            (page.BindingContext as ViewModelBase).Initialize(param);
            return NavigatePushAsync(page);
        }

        public void NavigateToLogin()
        {
            //CurrentApplication.MainPage = new LoginView();
            throw new NotImplementedException();
        }

        public void NavigateToMain()
        {
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                CurrentApplication.MainPage = new MainPageView();
            }
            else
            {
                CurrentApplication.MainPage = new MainPagePhoneView();
            }
        }

        public void NavigateToTicketAccess()
        {
            CurrentApplication.MainPage = new TicketAccessControlView();  
        }
    }
}
