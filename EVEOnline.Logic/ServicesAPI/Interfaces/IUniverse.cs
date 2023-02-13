using EVEOnline.Logic.ModelsAPI;

namespace EVEOnline.Logic.ServicesAPI.Interfaces
{
    public interface IUniverse
    {
        public Task<int[]> GetUniverseAsync(string argUniverse);
        public Task<URegion> GetRegionAsync(int id, string? language);
        public Task<UConstellation> GetConstellationAsync(int id, string? language);
        public Task<USystem> GetSystemAsync(int id, string? language);
        public Task<UniverseTypeModel> GetUniverseTypeAsync(int id,string? language, bool addBD);
        public Task<UniverseTypeModel> GetUniverseTypeBSAsync(int id, string? language, bool addBD);
        public Task<UniverseStationModel> GetUniverseStationAsync(long id, string? language, bool addBD);
    }
}
