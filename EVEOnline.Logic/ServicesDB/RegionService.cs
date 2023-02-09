﻿using EVEOnline.Data.Models;
using EVEOnline.Logic.ServicesDB.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVEOnline.Logic.ServicesDB
{
    public class RegionService : IRegionService
    {
        private readonly MyEveonlineDbContext _db;
        public RegionService(MyEveonlineDbContext db) => _db = db;
        public async Task<Uregion> GetRegion(int id)
        {
            if (id > 0)
            {
                //var response = await _db.UniverseRegions.FirstOrDefaultAsync(x => x.Id == id);
                var response = await _db.Uregions.FirstOrDefaultAsync(x => x.Id == id);

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
        }


        public async Task<int> PostRegion(Uregion newRegion)
        {
            _db.Add(newRegion);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            var regionId = newRegion.Id;

            return regionId;
        }
    }
}
