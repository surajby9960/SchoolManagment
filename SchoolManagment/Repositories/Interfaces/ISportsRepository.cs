using SchoolManagment.Model;

namespace SchoolManagment.Repositories.Interfaces
{
    public interface ISportsRepository
    {
        public Task<int> AddSport(Sports sports);
        public Task<int> UpdateSport(Sports sports);
        public Task<int> DeleteSport( int id);
        public Task<IEnumerable<Sports>> GetAllSport();
        public Task<Sports> GetSportById(int id);

    }
}
