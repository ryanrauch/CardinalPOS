using CardinalPOS.Repositories.Interfaces;
using CardinalPOS.Services.Interfaces;
using CardinalPOSLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardinalPOS.Repositories
{
    public class TabRepository : ITabRepository
    {
        private readonly IRequestService _requestService;

        private readonly string _uri = "api/Tabs/";
        private List<Tab> _collection { get; set; }
        private bool _initialized { get; set; }

        public TabRepository(IRequestService requestService)
        {
            _requestService = requestService;
            _initialized = false;
        }

        public async Task AddAsync(Tab tab)
        {
            if(!_collection.Contains(tab))
            {
                _collection.Add(tab);
                await _requestService.PostAsync(_uri, tab);
            }
        }

        public async Task<Tab> GetByIdAsync(Guid tabId)
        {
            await InitializeAsync();
            var item = _collection.FirstOrDefault(t => t.TabId.Equals(tabId));
            if(item != null)
            {
                return item;
            }
            Tab remote = await _requestService.GetAsync<Tab>(_uri + tabId.ToString());
            if(remote != null)
            {
                _collection.Add(remote);
                return remote;
            }
            return null;
        }

        public async Task<List<Tab>> GetAllAsync()
        {
            await InitializeAsync();
            return _collection;
        }

        public async Task InitializeAsync()
        {
            if (!_initialized)
            {
                _collection = await _requestService.GetAsync<List<Tab>>(_uri);
                _initialized = true;
            }
        }
    }
}
