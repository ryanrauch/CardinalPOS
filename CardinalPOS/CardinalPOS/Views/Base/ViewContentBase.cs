using Autofac;
using CardinalPOS.ViewModels.Base;
using Xamarin.Forms;

namespace CardinalPOS.Views.Base
{
    public class ViewContentBase<T> : ContentView where T : ViewModelBase
    {
        private readonly T _viewModel;
        public T ViewModel
        {
            get { return _viewModel; }
        }

        public ViewContentBase()
        {
            using (var scope = App.Container.BeginLifetimeScope())
            {
                _viewModel = App.Container.Resolve<T>();
            }
            BindingContext = _viewModel;
        }
    }
}
