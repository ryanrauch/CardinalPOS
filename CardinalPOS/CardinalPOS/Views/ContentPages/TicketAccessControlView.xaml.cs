using CardinalPOS.ViewModels;
using CardinalPOS.Views.Base;
using Xamarin.Forms.Xaml;

namespace CardinalPOS.Views.ContentPages
{
    public class TicketAccessControlViewBase : ViewPageBase<TicketAccessControlViewModel> { }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TicketAccessControlView : TicketAccessControlViewBase
	{
		public TicketAccessControlView ()
		{
			InitializeComponent ();
		}
	}
}