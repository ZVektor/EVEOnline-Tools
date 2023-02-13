using EVEOnline.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOnline.Logic.ServicesDB.Interfaces
{
    public interface IUniverseTypeBSServiceDB
    {
        public Task<List<TbUniverseTypeB>> GetUniverseTypesBS();
        public Task<TbUniverseTypeB> GetUniverseTypeBS(int id);
        public Task<int> PostUniverseTypeBS(TbUniverseTypeB newUniverseType);
    }
}
