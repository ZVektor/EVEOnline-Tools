using EVEOnline.Data.Models;
using EVEOnline.Logic.ServicesDB.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface IUniverseTypeServiceDB
    {
        public Task<List<TbUniverseType>> GetUniverseTypes();
        public Task<TbUniverseType> GetUniverseType(int id);
        public Task<int> PostUniverseType(TbUniverseType newUniverseType);
        //public Task<bool> UpdateUniverseType(int id, TbUniverseType updateUniverseType);
        //public Task<bool> DeleteUniverseType(int id);
    }
}
