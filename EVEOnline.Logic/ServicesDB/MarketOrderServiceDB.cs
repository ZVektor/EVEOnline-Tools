using EVEOnline.Data.Models;
using EVEOnline.Logic.ServicesDB.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesDB
{
    public class MarketOrderServiceDB : IMarketOrderServiceDB
    {
        private readonly MyEveonlineDbContext _db;
        public MarketOrderServiceDB(MyEveonlineDbContext db) => _db = db;

        public async Task<List<TbMarketOrder>> GetMarketOrders()
        {
            return await _db.TbMarketOrders.ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<TbMarketOrder> GetMarketOrder(long id)
        {
            if (id > 0)
            {
                //var response = await _db.UniverseRegions.FirstOrDefaultAsync(x => x.Id == id);
                var response = await _db.TbMarketOrders.FirstOrDefaultAsync(x => x.Id == id);

                if (response == null)
                {
                    return null;
                }
                return response;
            }
            else
            {
                return null;
            }
            //throw new NotImplementedException();
        }

        public async Task<long> PostMarketOrder(TbMarketOrder newMarketOrder)
        {
            _db.Add(newMarketOrder);
            await _db.SaveChangesAsync();
            var marketOrderId = newMarketOrder.Id;

            return marketOrderId;
            //throw new NotImplementedException();
        }
    }
}
