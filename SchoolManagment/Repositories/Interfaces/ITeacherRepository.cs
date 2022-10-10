using SchoolManagment.Model;

namespace SchoolManagment.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<IEnumerable<Teacher>> GetTeacher();
        public Task<int> AddTeacher(Teacher data);
        public Task<int> UpdateTeacher(Teacher teacher);
        public Task<int> DeleteTeacher(int tid,int sid);
        public Task<Teacher> GetTeacherById(int id);
        // public Task<Teacher>countteacher(string name);

        public Task<BaseResponse> GetTeacherByPagination(int pageno, int pagsize, string? name);

    }
}
