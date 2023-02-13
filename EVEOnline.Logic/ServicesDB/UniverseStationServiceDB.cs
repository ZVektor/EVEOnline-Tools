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
    public class UniverseStationServiceDB : IUniverseStationServiceDB
    {
        private readonly MyEveonlineDbContext _db;
        public UniverseStationServiceDB(MyEveonlineDbContext db) => _db = db;

        public async Task<List<TbUniverseStation>> GetUniverseStations()
        {
            return await _db.TbUniverseStations.ToListAsync();
            //throw new NotImplementedException();
        }
        public async Task<TbUniverseStation> GetUniverseStation(long id)
        {
            if (id > 0)
            {
                //var response = await _db.UniverseRegions.FirstOrDefaultAsync(x => x.Id == id);
                var response = await _db.TbUniverseStations.FirstOrDefaultAsync(x => x.Id == id);

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
        public async Task<long> PostUniverseStation(TbUniverseStation newUniverseStation)
        {
            _db.Add(newUniverseStation);
            await _db.SaveChangesAsync();
            var universeStationId = newUniverseStation.Id;

            return universeStationId;
            //throw new NotImplementedException();
        }
    }
}
