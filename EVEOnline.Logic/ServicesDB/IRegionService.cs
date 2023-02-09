using EVEOnline.Data.Models;
using System.Numerics;

namespace EVEOnline.Logic.Services
{
    public interface IRegionService
    {
        //public Task<IEnumerable<UniverseRegion>> GetRegions();
        public Task<Uregion> GetRegion(int id);
        public Task<int> PostRegion(Uregion newRegion);

        //public Task<bool> UpdateRegion(int id, UniverseRegion updateRegion);

        //public Task<bool> DeleteRegion(int id);

    }
}
