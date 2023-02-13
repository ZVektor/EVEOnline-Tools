using EVEOnline.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface IMarketOrderServiceDB
    {
        public Task<List<TbMarketOrder>> GetMarketOrders();
        public Task<TbMarketOrder> GetMarketOrder(long id);
        public Task<long> PostMarketOrder(TbMarketOrder newMarketOrder);

        //public Task<bool> UpdateRegion(int id, Uregion updateRegion);

        //public Task<bool> DeleteRegion(int id);
    }
}
