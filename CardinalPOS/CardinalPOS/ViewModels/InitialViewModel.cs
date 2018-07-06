using CardinalPOS.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardinalPOS.ViewModels
{
    public class InitialViewModel : ViewModelBase
    {
        public override Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }
    }
}
