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
    public class UniverseTypeServiceDB : IUniverseTypeServiceDB
    {
        private readonly MyEveonlineDbContext _db;
        public UniverseTypeServiceDB(MyEveonlineDbContext db) => _db = db;

        public async Task<List<TbUniverseType>> GetUniverseTypes()
        {
            return await _db.TbUniverseTypes.ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<TbUniverseType> GetUniverseType(int id)
        {
            if (id > 0)
            {
                //var response = await _db.UniverseRegions.FirstOrDefaultAsync(x => x.Id == id);
                var response = await _db.TbUniverseTypes.FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task<int> PostUniverseType(TbUniverseType newUniverseType)
        {
            _db.Add(newUniverseType);
            await _db.SaveChangesAsync();
            var universeTypeId = newUniverseType.Id;

            return universeTypeId;
            //throw new NotImplementedException();
        }
    }
}
