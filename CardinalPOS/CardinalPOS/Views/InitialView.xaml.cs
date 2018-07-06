using CardinalPOS.ViewModels;
using CardinalPOS.Views.Base;
using Xamarin.Forms.Xaml;

namespace CardinalPOS.Views
{
    public class InitialViewBase : ViewPageBase<InitialViewModel> { }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InitialView : InitialViewBase
	{
		public InitialView ()
		{
			InitializeComponent ();
		}
	}
}