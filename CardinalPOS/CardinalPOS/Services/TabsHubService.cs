using CardinalPOS.Services.Interfaces;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardinalPOS.Services
{
    public class TabsHubService : ITabsHubService
    {
        private HubConnection _connection { get; set; }

        public event EventHandler OnReceived;

        public async Task InitializeHub()
        {
            try
            {
                _connection = new HubConnection("http://localhost:58402/TabsHub");
                await _connection.Start();
                _connection.Received += connection_Received;
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert(ex.Message, ex.StackTrace, "err");
            }
        }

        private void connection_Received(string obj)
        {
            App.Current.MainPage.DisplayAlert("recv", obj, "ok");
        }
    }
}
