using CardinalPOS.ViewModels;
using CardinalPOS.Views.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardinalPOS.Views.ContentPages
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