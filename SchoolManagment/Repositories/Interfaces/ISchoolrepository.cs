using SchoolManagment.Model;

namespace SchoolManagment.Repositories.Interfaces
{
    public interface ISchoolrepository
    {
        public Task<BaseResponse> GetSchool(int pageno,int pagesize,string? schoolname);
        public Task<int> AddSchool(School school);
        public Task<int> UpdateSchool(School school);
        public Task<int> DeleteSchool(int id);
        public Task<School> GetSchoolById(int id);

    }
   /* public interface IClassRepository
    {
        public Task<int> AddClass(List<Class> cls, int sid);
    }
    public interface ITeacherRepository
    {
        public Task<int> AddTeacher(List<Teacher> teachers, int res);
    }*/
}
