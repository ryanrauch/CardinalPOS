using System.Threading.Tasks;
using Xamarin.Forms;

namespace CardinalPOS.Services.Interfaces
{
    public interface INavigationService
    {
        void NavigateToMain();
        void NavigateToTicketAccess();
        void NavigateToLogin();
        Task NavigatePopAsync();
        Task NavigatePushAsync<T>(T page) where T : Page;
        Task NavigatePushAsync<T>(T page, object param) where T : Page;
    }
}
