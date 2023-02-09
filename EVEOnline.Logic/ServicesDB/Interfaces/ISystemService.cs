using EVEOnline.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface ISystemService
    {
        //public Task<IEnumerable<Usystem>> GetSystems();
        public Task<Usystem> GetSystem(int id);
        public Task<int> PostSystem(Usystem newSystem);

        //public Task<bool> UpdateSystem(int id, Usystem updateSystem);

        //public Task<bool> DeleteSystem(int id);
    }
}
