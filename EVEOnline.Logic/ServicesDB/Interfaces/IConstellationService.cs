using EVEOnline.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface IConstellationService
    {
        //public Task<IEnumerable<TbUniverseConstellation>> GetConstellations();
        public Task<TbUniverseConstellation> GetConstellation(int id);
        public Task<int> PostConstellation(TbUniverseConstellation newConstellation);

        //public Task<bool> UpdateConstellation(int id, TbUniverseConstellation updateConstellation);

        //public Task<bool> DeleteConstellation(int id);
    }
}
