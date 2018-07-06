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

            Tabs = new ObservableCollection<TabModel>();
            Items = new ObservableCollection<ItemModel>();
            QuickItems = new ObservableCollection<ItemModel>();
            Clock = DateTime.Now;
            Device.StartTimer(TimeSpan.FromMilliseconds(100), OnClockTick);
        }

        private DateTime _clock { get; set; }
        public DateTime Clock
        {
            get { return _clock; }
            set
            {
                _clock = value;
                RaisePropertyChanged(() => Clock);
                RaisePropertyChanged(() => ClockHourMinute);
                RaisePropertyChanged(() => ClockDate);
            }
        }

        private bool OnClockTick()
        {
            Clock = DateTime.Now;
            return true;
        }

        public string ClockHourMinute => Clock.ToString("hh:mm:ss tt");
        public string ClockDate => Clock.ToString("MM/dd/yyyy");

        //private ObservableCollection<GroupedTabModel> _groupedTabs { get; set; }
        //public ObservableCollection<GroupedTabModel> GroupedTabs
        //{
        //    get { return _groupedTabs; }
        //    set
        //    {
        //        _groupedTabs = value;
        //        RaisePropertyChanged(() => GroupedTabs);
        //    }
        //}

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

        private ObservableCollection<ItemModel> _quickItems { get; set; }
        public ObservableCollection<ItemModel> QuickItems
        {
            get { return _quickItems; }
            set
            {
                _quickItems = value;
                RaisePropertyChanged(() => QuickItems);
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
            if(Tabs.Any(t => t.TabId.Equals(e.EventTab.TabId)))
            {
                return;
            }
            bool ins = false;
            for(int i = Tabs.Count; i > 0; --i)
            {
                if(String.Compare(Tabs[i - 1].LastName,
                                  e.EventTab.LastName,
                                  ignoreCase: true) < 0)
                {
                    Tabs.Insert(i, new TabModel(e.EventTab));
                    ins = true;
                    break;
                }
            }
            if (!ins)
            {
                Tabs.Add(new TabModel(e.EventTab));
            }
            //string shortName = e.EventTab.LastName.Substring(0, 1);
            //var g = GroupedTabs.FirstOrDefault(t => t.ShortName.Equals(shortName));
            //if (g != null)
            //{
            //    g.Add(new TabModel(e.EventTab));
            //}
            //else
            //{
            //    GroupedTabModel gtm = new GroupedTabModel()
            //    {
            //        ShortName = shortName,
            //        LongName = shortName
            //    };
            //    gtm.Add(new TabModel(e.EventTab));
            //    GroupedTabs.Add(gtm);
            //    var sorted = GroupedTabs.OrderBy(gt => gt.LongName);
            //    GroupedTabs = new ObservableCollection<GroupedTabModel>(sorted);
            //}
        }
        public ICommand TabDeselectCommand => new Command(TabDeselectCommandFunction);
        private void TabDeselectCommandFunction()
        {
            TabsSelectedItem = null;
        }

        public ICommand TabPrintCommand => new Command(async () => await TabPrintCommandFunction());
        private async Task TabPrintCommandFunction()
        {
            await Task.Delay(100);
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
            TabsSelectedItem = null;
        }

        public ICommand RemoveTabCommand => new Command(async () => await RemoveTabCommandFunction());
        private async Task RemoveTabCommandFunction()
        {
            if(TabsSelectedItem != null)
            {
                var tsi = Tabs.FirstOrDefault(t => t.TabId.Equals(TabsSelectedItem.TabId));
                if (tsi != null)
                {
                    if(await _requestService.DeleteAsync<bool>("api/Tabs/" + tsi.TabId.ToString()))
                    {
                        Tabs.RemoveAt(Tabs.IndexOf(tsi));
                    }

                }
                //Guid gid = TabsSelectedItem.TabId;
                //string shortName = string.Empty;
                //if (TabsSelectedItem.LastName.Length >= 1)
                //{
                //    shortName = TabsSelectedItem.LastName.Substring(0, 1);
                //    var del = await _requestService.DeleteAsync<Tab>("api/Tabs/" + gid.ToString());
                //    var gt = GroupedTabs.FirstOrDefault(g => g.ShortName.Equals(shortName));
                //    if (gt != null)
                //    {
                //        var tm = gt.Remove(TabsSelectedItem);
                //    }
                //}
            }
            TabsSelectedItem = null;
        }


        public ICommand ItemButtonCommand => new Command<ItemModel>(async (im) => await ItemButtonCommandFunction(im));
        private async Task ItemButtonCommandFunction(ItemModel item)
        {
            ItemsSelectedItem = item;
            await Task.Delay(100);
        }

        public ICommand QuickItemButtonCommand => new Command<ItemModel>(async (im) => await QuickItemButtonCommandFunction(im));
        private async Task QuickItemButtonCommandFunction(ItemModel item)
        {
            ItemsSelectedItem = item;
            await Task.Delay(100);
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
            Tabs.Clear();
            foreach(var tm in tablist.OrderBy(t=>t.LastName))
            {
                Tabs.Add(new TabModel(tm));
            }

            //GroupedTabs = new ObservableCollection<GroupedTabModel>();
            //for (char c = 'A'; c <= 'Z'; c++)
            //{
            //    string shortName = c.ToString();
            //    GroupedTabModel gtm = new GroupedTabModel()
            //    {
            //        ShortName = shortName,
            //        LongName = shortName
            //    };
            //    bool found = false;
            //    foreach (var tm in tablist.Where(t => t.LastName.StartsWith(shortName)))
            //    {
            //        gtm.Add(new TabModel(tm));
            //        found = true;
            //    }
            //    if (found)
            //    {
            //        GroupedTabs.Add(gtm);
            //    }
            //}

            var itemslist = await _requestService.GetAsync<List<Item>>("api/Items/");
            Items.Clear();
            foreach (var i in itemslist)
            {
                Items.Add(new ItemModel(i));
            }

            QuickItems.Clear();
            foreach (var i in itemslist)
            {
                QuickItems.Add(new ItemModel(i));
            }

            for(int i = 1; i <= 10; ++i)
            {
                var item = new Item()
                {
                    ItemId = Guid.NewGuid(),
                    Description = "Drink Number " + i.ToString(),
                    Price = 1.25M + (Decimal)i,
                    ItemGroupId = itemslist.ElementAt(i % 4).ItemGroupId,
                    ItemGroup = itemslist.ElementAt(i % 4).ItemGroup
                };
                Items.Add(new ItemModel(item));
            }

            _tabsHubService.OnAddTab += tabsHubService_OnAddTab;
            await _tabsHubService.InitializeHub();
        }
    }
}
