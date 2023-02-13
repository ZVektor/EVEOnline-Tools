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
    public class СonstellationService : IConstellationService
    {
        private readonly MyEveonlineDbContext _db;
        public СonstellationService(MyEveonlineDbContext db) => _db = db;
        public async Task<TbUniverseConstellation> GetConstellation(int id)
        {
            if (id > 0)
            {
                var response = await _db.TbUniverseConstellations.FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task<int> PostConstellation(TbUniverseConstellation newConstellation)
        {
            _db.Add(newConstellation);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            var constellationId = newConstellation.Id;

            return constellationId;
            //throw new NotImplementedException();
        }
    }
}
