using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CardinalPOSLibrary.Models;

namespace CardinalPOS.Repositories.Interfaces
{
    public interface ITabRepository
    {
        Task AddAsync(Tab tab);
        Task<List<Tab>> GetAllAsync();
        Task<Tab> GetByIdAsync(Guid tabId);
        Task InitializeAsync();
    }
}