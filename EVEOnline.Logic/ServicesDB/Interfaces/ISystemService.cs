using EVEOnline.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface ISystemService
    {
        //public Task<IEnumerable<TbUniverseSystem>> GetSystems();
        public Task<TbUniverseSystem> GetSystem(int id);
        public Task<int> PostSystem(TbUniverseSystem newSystem);

        //public Task<bool> UpdateSystem(int id, TbUniverseSystem updateSystem);

        //public Task<bool> DeleteSystem(int id);
    }
}
