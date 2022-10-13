using SchoolManagment.Model;

namespace SchoolManagment.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        public Task<BaseResponse> GetStudent(int pageno, int pagesize, string? studname);
        public Task<int> AddStudent(Student student);
        public Task<int> UpdateStudent(Student student);
        public Task<int> DeleteStudent(int id);
        public Task<Student> GetStudeById(int id);
    }
}
