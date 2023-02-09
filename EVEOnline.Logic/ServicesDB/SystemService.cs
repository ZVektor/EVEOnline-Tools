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
    public class SystemService : ISystemService
    {
        private readonly MyEveonlineDbContext _db;
        public SystemService(MyEveonlineDbContext db) => _db = db;
        public async Task<Usystem> GetSystem(int id)
        {
            if (id > 0)
            {
                var response = await _db.Usystems.FirstOrDefaultAsync(x => x.Id == id);

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

        public async Task<int> PostSystem(Usystem newSystem)
        {
            _db.Add(newSystem);
            await _db.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            var regionId = newSystem.Id;

            return regionId;
            //throw new NotImplementedException();
        }
    }
}
