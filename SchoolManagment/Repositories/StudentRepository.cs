using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;
using System.Data.Common;
using Dapper;

namespace SchoolManagment.Repositories
{
    public class StudentRepository : BaseAsyncRepository, IStudentRepository
    {
        public StudentRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<int> AddStudent(Student student)
        {

            var qry = "insert into tblstudent(studname ,studdob ,studaddress ,studgender ,admissiondate, sportid, classid ,isdeleted)values" +
                " (@studname ,@studdob ,@studaddress ,@studgender ,@admissiondate, @sportid, @classid ,0)";
            using (DbConnection dbConnection= WriterConnection)
            {
                student.admissiondate = DateTime.Now;
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, student);
                return res;
            }
            
        }

        public async Task<int> DeleteStudent(int id)
        {
            var qry = "Update tblstudent set isdeleted=1 where studentid=@id";
            using (DbConnection dbConnection = WriterConnection)
            {

                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, new { id });
                return res;
            }
        }

        public async Task<Student> GetStudeById(int id)
        {
            var qry = "Select  * from tblstudent where studentid=@id";
            using (DbConnection dbConnection = WriterConnection)
            {

                await dbConnection.OpenAsync();
                var res = await dbConnection.QuerySingleAsync(qry, new { id });
                return res;
            }
        }

        public async Task<BaseResponse> GetStudent(int pageno, int pagesize, string? studname)
        {
            BaseResponse baseResponse = new BaseResponse();
            PaginationModel paginationModel=new PaginationModel();
            List<Student> students = new List<Student>();
            if(pageno==0)
            {
                pageno = 1;
            }
            if(pagesize==0)
            {
                pagesize = 10;
            }
            var val = (pageno - 1) * pagesize;
            var qry = @"select * from tblstudent where (studname like '%'+@studname+'%' or studname is null) and isdeleted=0 order by studid desc
                  offset @val rows fetch next @pagesize rows only;
                 select @pageno as pageno,count(distinct studid) as totalpages from tblstudent";
            using(DbConnection dbConnection = WriterConnection)
            {
                await dbConnection.OpenAsync();
                var par = new { pageno, pagesize, val, studname };
                var res = await dbConnection.QueryMultipleAsync(qry, par);
               var stud = await res.ReadAsync<Student>();
                students = stud.ToList();
                var pagination = await res.ReadAsync<PaginationModel>();
                paginationModel = pagination.FirstOrDefault();
                int last = 0;
                int pagecount = 0;
                last = paginationModel.Totalpages % pagesize;
                pagecount=paginationModel.Totalpages / pagesize;
                paginationModel.Pagecount = paginationModel.Totalpages;
                paginationModel.Totalpages = pagecount;
                if(last>0)
                {
                    paginationModel.Totalpages = pagecount + 1;
                }
                baseResponse.ResponseData = students;
                baseResponse.ResponseData1 = paginationModel;
                return baseResponse;


            }
        }

        public async Task<int> UpdateStudent(Student student)
        {
            var qry = "Update tblstudent set studname=@studname ,studdob=@studdob ,studaddress=@studaddress ,studgender=@studgender" +
                " sportid=@sportid, classid=@classid where studentid=@id";
            using (DbConnection dbConnection = WriterConnection)
            {
                
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, student);
                return res;
            }
        }
    }
}
