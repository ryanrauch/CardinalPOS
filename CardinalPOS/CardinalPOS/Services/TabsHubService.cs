/*using CardinalPOS.Services.Interfaces;
using CardinalPOSLibrary.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace CardinalPOS.Services
{
    public class TabsHubService : ITabsHubService
    {
        private HubConnection _connection { get; set; }

        public event EventHandler<AddTabEventArgs> OnAddTab;

        public async Task InitializeHub()
        {
            try
            {
                //_connection = new HubConnectionBuilder()
                //                  .WithUrl("http://localhost:58403/TabsHub/")
                //                  .Build();
                _connection = new HubConnectionBuilder()
                                  .WithUrl(Constants.CardinalWebApiBase + "TabsHub/")
                                  .Build();

                _connection.On<string, string, string, string>("AddTab", (tabId, fn, ln, dt) => 
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var t = new Tab()
                        {
                            TabId = Guid.NewGuid(),
                            FirstName = fn,
                            LastName = ln,
                            TimestampOpened = DateTime.Now
                        };
                        OnAddTab?.Invoke(this, new AddTabEventArgs(t));
                    });
                });
                //_connection.On<string, string>("ReceiveMessage", (user, msg) =>
                //{
                //    Device.BeginInvokeOnMainThread(() =>
                //    {
                //        connection_Received(user + " --- " + msg);
                //    });
                //});
                await _connection.StartAsync();
                //await Task.Delay(3000);
                //await _connection.InvokeAsync("SendMessage", "ryan", "finally");
            }
            catch(Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.DisplayAlert("TabsHubService::Error", ex.Message, "OK");
                });
            }
        }
        //protected virtual void OnAddTabEvent(AddTabEventArgs e)
        //{
        //    OnAddTab?.Invoke(this, e);
        //}
        //private void connection_Received(string obj)
        //{
        //    App.Current.MainPage.DisplayAlert("recv", obj, "ok");
        //}
    }
}
*/