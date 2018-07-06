using CardinalPOS.ViewModels;
using CardinalPOS.Views.Base;
using Xamarin.Forms.Xaml;

namespace CardinalPOS.Views.ContentPages
{
    public class MainPageViewBase : ViewPageBase<MainPageViewModel> { }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPageView : MainPageViewBase
	{
		public MainPageView ()
		{
			InitializeComponent ();
		}
	}
}