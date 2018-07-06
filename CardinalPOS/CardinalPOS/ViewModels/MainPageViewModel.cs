using CardinalPOS.Models;
using CardinalPOS.Repositories.Interfaces;
using CardinalPOS.Services.Interfaces;
using CardinalPOS.ViewModels.Base;
using CardinalPOSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CardinalPOS.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IRequestService _requestService;

        public MainPageViewModel(
            IRequestService requestService)
        {
            _requestService = requestService;
        }

        private ObservableCollection<GroupedTabModel> _groupedTabs { get; set; }
        public ObservableCollection<GroupedTabModel> GroupedTabs
        {
            get { return _groupedTabs; }
            set
            {
                _groupedTabs = value;
                RaisePropertyChanged(() => GroupedTabs);
            }
        }

        private ObservableCollection<TabModel> _tabs { get; set; }
        public ObservableCollection<TabModel> Tabs
        {
            get { return _tabs; }
            set
            {
                _tabs = value;
                RaisePropertyChanged(() => Tabs);
            }
        }

        private TabModel _tabsSelectedItem { get; set; }
        public TabModel TabsSelectedItem
        {
            get { return _tabsSelectedItem; }
            set
            {
                _tabsSelectedItem = value;
                RaisePropertyChanged(() => TabsSelectedItem);
            }
        }

        private ObservableCollection<ItemModel> _items { get; set; }
        public ObservableCollection<ItemModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }
        private ItemModel _itemsSelectedItem { get; set; }
        public ItemModel ItemsSelectedItem
        {
            get { return _itemsSelectedItem; }
            set
            {
                _itemsSelectedItem = value;
                RaisePropertyChanged(() => ItemsSelectedItem);
            }
        }
        public override async Task OnAppearingAsync()
        {
            var tablist = await _requestService.GetAsync<List<Tab>>("api/Tabs/");
            Tabs = new ObservableCollection<TabModel>();
            foreach(var t in tablist.OrderBy(t=>string.Format("{0}, {1}", t.LastName, t.FirstName)))
            {
                Tabs.Add(new TabModel(t));
            }
            
            GroupedTabs = new ObservableCollection<GroupedTabModel>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string shortName = c.ToString();
                GroupedTabModel gtm = new GroupedTabModel()
                {
                    ShortName = shortName,
                    LongName = shortName
                };
                bool found = false;
                foreach (var tm in Tabs.Where(t => t.LastName.StartsWith(shortName)))
                {
                    gtm.Add(tm);
                    found = true;
                }
                if (found)
                {
                    GroupedTabs.Add(gtm);
                }
            }

            var itemslist = await _requestService.GetAsync<List<Item>>("api/Items/");
            Items = new ObservableCollection<ItemModel>();
            foreach(var i in itemslist)
            {
                Items.Add(new ItemModel(i));
            }
        }
    }
}
