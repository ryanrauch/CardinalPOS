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
        }

        public Guid TabId => _item.TabId;
        public string FirstName => _item.FirstName;
        public string LastName => _item.LastName;
        public string DisplayName => string.Format("{0}, {1}", _item.LastName,
                                                               _item.FirstName);
        public string MinutesOld
        {
            get
            {
                var span = DateTime.Now - _item.TimestampOpened;
                return string.Format("{0}mins ago", Math.Max(0,(int)span.TotalMinutes));
            }
        }
    }
}
