using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;
using System.Data.Common;
using Dapper;
using System;

namespace SchoolManagment.Repositories
{
    public class TeacherRepository : BaseAsyncRepository,ITeacherRepository
    {
        public TeacherRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddTeacher(Teacher data)
        {
            var qry = " insert into tblteacher(schoolid, teachername, teachermobile ,teacheremail ,teacheraddress ," +
                      " teacherjoindate ,teachersubject,isdeleted)" +
                      " values(@schoolid, @teachername, @teachermobile ,@teacheremail ," +
                      " @teacheraddress ,@teacherjoindate ,@teachersubject,0) ";

            using (DbConnection dbConnection = WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, data);
                var nofteach = await dbConnection.QuerySingleAsync<int>("select noofteacher from tblschool where schoolid=@schoolid", new {data.schoolid });
                var updnoofteacher = await dbConnection.ExecuteAsync("update tblschool set noofteacher=(@nofteach+1) where schoolid=@schoolid", new { data.schoolid,nofteach });
              return (int)res;
            }
        
                

            
            
        }

        public async Task<int> DeleteTeacher(int tid, int sid)
        {

            var qry = "update tblteacher set isdeleted=1,modifydate=@modifydate where schoolid=@sid and teacherid=@tid";
            using (DbConnection dbConnection = WriterConnection)
            {
                DateTime md=DateTime.Now;
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, new { tid, sid, modifydate=md });
               
                return res;

            }
        }

        public async Task<IEnumerable<Teacher>> GetTeacher()
        {
            List<Teacher> teachers = new List<Teacher>();
            var qry = "select * from tblteacher";
            using (DbConnection dbConnection = ReaderConnection)
            {
                await dbConnection.OpenAsync();
                var teace = await dbConnection.QueryAsync<Teacher>(qry);
                teachers = teace.ToList();
                return teachers;
            }
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            var qry = "select * from tblteacher where teacherid=@id";
            using (DbConnection dbConnection = ReaderConnection)
            {
                await dbConnection.OpenAsync();
                var teacher = await dbConnection.QuerySingleAsync<Teacher>(qry, new { id });
                return teacher;
            }
        }

        public async Task<BaseResponse> GetTeacherByPagination(int pageno, int pagsize, string? name)
        {
            BaseResponse baseResponse = new BaseResponse();
            List<ViewTeacher> teachers = new List<ViewTeacher>();
            PaginationModel paginationModel = new PaginationModel();
            if (pageno == 0)
            {
                pageno = 1;
            }
            if (pagsize == 0)
            {
                pagsize = 10;
            }
            var val = (pageno - 1) * pagsize;
            using (DbConnection dbConnection = ReaderConnection)
            {
                await dbConnection.OpenAsync();
                var qry = @"select ROW_NUMBER() OVER(order by teacherid desc) as SrNo,*from tblteacher order by teacherid desc
                        OFFSET @val rows fetch next @Pagesize rows only;
                        select @Pageno as Pageno,count(distinct teacherid) as totalpages from tblteacher";
                var par = new { pageno = pageno, pagesize = pagsize, val = val };
                var result=await dbConnection.QueryMultipleAsync(qry, par);
                var teacher1 = await result.ReadAsync<ViewTeacher>();
                var paginations=await result.ReadAsync<PaginationModel>();
                  paginationModel = paginations.FirstOrDefault();
                teachers = teacher1.ToList();
                var last = 0;
                var pagecount = 0;
                last = paginationModel.Totalpages % pagsize;
                pagecount= paginationModel.Totalpages / pagsize;
                paginationModel.Pagecount = paginationModel.Totalpages;
                paginationModel.Totalpages = pagecount;
                if(last >0)
                {
                    paginationModel.Totalpages = pagecount+1;
                }
                baseResponse.ResponseData = paginationModel;
                baseResponse.ResponseData1 = teachers;
                return (baseResponse);
            }
        }

        public async Task<int> UpdateTeacher(Teacher teacher)
        {
            var qry = "update tblteacher set teachername=@teachername, teachermobile=@teachermobile teacheremail=@teacheremail" +
                " teacheraddress=@teacheraddress ,teachersubject=@teachersubject where teacherid=@teacherid)";
            using(DbConnection dbConnection = ReaderConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, teacher);
                return res;
            }
        }

    }
}
