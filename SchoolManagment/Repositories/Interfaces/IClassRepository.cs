using SchoolManagment.Model;

namespace SchoolManagment.Repositories.Interfaces
{
    public interface IClassRepository
    {
        public Task<int> AddClass(InsertClass cls);
        public Task <int> UpdateClass(UpdateClass cls);
        public Task<int> DeleteClass(int cid, int sid);
        public Task<IEnumerable<InsertClass>> GetAllClasses();
        public Task<UpdateClass> GetClassById(int id);
        
    }
}
