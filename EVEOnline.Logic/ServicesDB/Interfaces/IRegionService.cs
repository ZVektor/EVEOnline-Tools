using EVEOnline.Data.Models;
using System.Numerics;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface IRegionService
    {
        public Task<List<TbUniverseRegion>> GetRegions();
        public Task<TbUniverseRegion> GetRegion(int id);
        public Task<int> PostRegion(TbUniverseRegion newRegion);

        //public Task<bool> UpdateRegion(int id, Uregion updateRegion);

        //public Task<bool> DeleteRegion(int id);

    }
}
