using EVEOnline.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface IUniverseStationServiceDB
    {
        public Task<List<TbUniverseStation>> GetUniverseStations();
        public Task<TbUniverseStation> GetUniverseStation(long id);
        public Task<long> PostUniverseStation(TbUniverseStation newUniverseStation);
        //public Task<bool> UpdateUniverseStation(int id, TbUniverseStation updateUniverseStation);
        //public Task<bool> DeleteUniverseStation(int id);
    }
}
