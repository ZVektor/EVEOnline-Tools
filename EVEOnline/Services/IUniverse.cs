using EVEOnline.Data.Models;
using EVEOnline.Logic.Models;
using System.Drawing;


namespace EVEOnline.Services
{
    public interface IUniverse
    {
        public Task<URegion> GetRegion(int id); 
        public Task<UConstellation> GetConstellation(int id);
        public Task<USystem> GetSystem(int id);
    }
}
