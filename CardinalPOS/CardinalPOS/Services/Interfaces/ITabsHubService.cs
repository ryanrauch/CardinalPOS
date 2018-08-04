using CardinalPOSLibrary.Models;
using System;
using System.Threading.Tasks;

namespace CardinalPOS.Services.Interfaces
{
    public interface ITabsHubService
    {
        event EventHandler<AddTabEventArgs> OnAddTab;
        Task InitializeHub();
    }

    public class AddTabEventArgs : EventArgs
    {
        public Tab EventTab { get; set; }
        public AddTabEventArgs(Tab t)
        {
            EventTab = t;
        }
    }
}