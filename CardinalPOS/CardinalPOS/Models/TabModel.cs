using CardinalPOS.ViewModels.Base;
using CardinalPOSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardinalPOS.Models
{
    public class TabModel : ExtendedBindableObject
    {
        private readonly Tab _item;

        public TabModel(Tab item)
        {
            _item = item;
            FirstName = _item.FirstName;
        }

        private string _firstName { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }
        public string LastName => _item.LastName;
        public string DisplayName => string.Format("{0}, {1}", _item.LastName,
                                                               _item.FirstName);
    }
}
