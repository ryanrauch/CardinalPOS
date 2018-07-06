using System;
using System.Threading.Tasks;

namespace CardinalPOS.Services.Interfaces
{
    public interface ITabsHubService
    {
        event EventHandler OnReceived;
        Task InitializeHub();
    }
}