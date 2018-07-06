using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CardinalPOS.Models
{
    public class GroupedTabModel : ObservableCollection<TabModel>
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
    }
}
