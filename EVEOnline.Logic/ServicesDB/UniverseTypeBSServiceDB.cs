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
    public class UniverseTypeBSServiceDB : IUniverseTypeBSServiceDB
    {
        private readonly MyEveonlineDbContext _db;
        public UniverseTypeBSServiceDB(MyEveonlineDbContext db) => _db = db;

        public async Task<List<TbUniverseTypeB>> GetUniverseTypesBS()
        {
            return await _db.TbUniverseTypeBs.ToListAsync();
        }
        public async Task<TbUniverseTypeB> GetUniverseTypeBS(int id)
        {
            if (id > 0)
            {
                var response = await _db.TbUniverseTypeBs.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<int> PostUniverseTypeBS(TbUniverseTypeB newUniverseTypeBS)
        {
            _db.Add(newUniverseTypeBS);
            await _db.SaveChangesAsync();
            var universeTypeId = newUniverseTypeBS.Id;

            return universeTypeId;
        }
    }
}
