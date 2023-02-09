﻿using EVEOnline.Data.Models;
using System.Numerics;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface IRegionService
    {
        //public Task<IEnumerable<UniverseRegion>> GetRegions();
        public Task<Uregion> GetRegion(int id);
        public Task<int> PostRegion(Uregion newRegion);

        //public Task<bool> UpdateRegion(int id, Uregion updateRegion);

        //public Task<bool> DeleteRegion(int id);

    }
}