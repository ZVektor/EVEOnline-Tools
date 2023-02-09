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
        //public Task<IEnumerable<Uconstellation>> GetConstellations();
        public Task<Uconstellation> GetConstellation(int id);
        public Task<int> PostConstellation(Uconstellation newConstellation);

        //public Task<bool> UpdateConstellation(int id, Uconstellation updateConstellation);

        //public Task<bool> DeleteConstellation(int id);
    }
}
