using CardinalPOS.Models;
using CardinalPOS.Services.Interfaces;
using CardinalPOS.ViewModels.Base;
using CardinalPOSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CardinalPOS.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IRequestService _requestService;
        private readonly ITabsHubService _tabsHubService;

        public MainPageViewModel(
            ITabsHubService tabsHubService,
            IRequestService requestService)
        {
            _tabsHubService = tabsHubService;
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

        //private ObservableCollection<TabModel> _tabs { get; set; }
        //public ObservableCollection<TabModel> Tabs
        //{
        //    get { return _tabs; }
        //    set
        //    {
        //        _tabs = value;
        //        RaisePropertyChanged(() => Tabs);
        //    }
        //}

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
        

        private void tabsHubService_OnAddTab(object sender, Services.AddTabEventArgs e)
        {
            string shortName = e.EventTab.LastName.Substring(0, 1);
            var g = GroupedTabs.FirstOrDefault(t => t.ShortName.Equals(shortName));
            if (g != null)
            {
                g.Add(new TabModel(e.EventTab));
            }
            else
            {
                GroupedTabModel gtm = new GroupedTabModel()
                {
                    ShortName = shortName,
                    LongName = shortName
                };
                gtm.Add(new TabModel(e.EventTab));
                GroupedTabs.Add(gtm);
                var sorted = GroupedTabs.OrderBy(gt => gt.LongName);
                GroupedTabs = new ObservableCollection<GroupedTabModel>(sorted);
            }
        }

        public ICommand AddTabCommand => new Command(async () => await AddTabCommandFunction());
        private async Task AddTabCommandFunction()
        {
            var t = new Tab()
            {
                TabId = Guid.NewGuid(),
                FirstName = "Ryan",
                LastName = "Rauch",
                TimestampOpened = DateTime.Now
            };
            await _requestService.PostAsync("api/Tabs/", t);
        }

        public ICommand RemoveTabCommand => new Command(async () => await RemoveTabCommandFunction());
        private async Task RemoveTabCommandFunction()
        {
            if(TabsSelectedItem != null)
            {
                Guid gid = TabsSelectedItem.TabId;
                string shortName = string.Empty;
                if (TabsSelectedItem.LastName.Length >= 1)
                {
                    shortName = TabsSelectedItem.LastName.Substring(0, 1);
                    var del = await _requestService.DeleteAsync<Tab>("api/Tabs/" + gid.ToString());
                    var gt = GroupedTabs.FirstOrDefault(g => g.ShortName.Equals(shortName));
                    if (gt != null)
                    {
                        var tm = gt.Remove(TabsSelectedItem);
                    }
                }
                TabsSelectedItem = null;
            }
        }

        public override async Task OnAppearingAsync()
        {
            /////////////////////////
            /////////////////////////
            MessagingCenter.Subscribe<MainPageViewModel, string>(this, "mainpagetest", 
                async (sender, args) => 
                {
                    await Task.Delay(2000);
                });

            MessagingCenter.Send(this, "mainpagetest", new EventArgs());

            MessagingCenter.Unsubscribe<MainPageViewModel, string>(this, "mainpagetest");
            /////////////////////////
            /////////////////////////
            var tablist = await _requestService.GetAsync<List<Tab>>("api/Tabs/");
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
                foreach (var tm in tablist.Where(t => t.LastName.StartsWith(shortName)))
                {
                    gtm.Add(new TabModel(tm));
                    found = true;
                }
                if (found)
                {
                    GroupedTabs.Add(gtm);
                }
            }

            var itemslist = await _requestService.GetAsync<List<Item>>("api/Items/");
            Items = new ObservableCollection<ItemModel>();
            foreach (var i in itemslist)
            {
                Items.Add(new ItemModel(i));
            }

            _tabsHubService.OnAddTab += tabsHubService_OnAddTab;
            await _tabsHubService.InitializeHub();
        }
    }
}
