using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;
using System.Data.Common;
using Dapper;
using System.Collections.Generic;

namespace SchoolManagment.Repositories
{
    public class SchoolRepository : BaseAsyncRepository, ISchoolrepository
    {
        public SchoolRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddSchool(School school)
        {
            List<InsertClass> cls=new List<InsertClass>();
            List<Teacher>teachers=new List<Teacher>();
            var qry = "insert into tblschool(schoolname, schoolgrade ,noofteacher, schooladdress , " +
                 "schooltelephone, schoolemail ,schooltype, schoolestablisheddate,isdeleted)values(@schoolname, @schoolgrade," +
                 " @noofteacher, @schooladdress,  @schooltelephone, @schoolemail, @schooltype, @schoolestablisheddate,0);" +
                 "select cast(scope_identity() as int)";
            school.schoolestablisheddate = DateTime.Now;
            using(DbConnection dbConnection=WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.QueryFirstOrDefaultAsync<int>(qry, school);

                cls = school.classes.ToList();
                teachers = school.teachers.ToList();
                await AddClass(cls, res);
                await AddTeacher(teachers, res);
                return res;
            }
        }

        public async Task AddTeacher(List<Teacher> teachers, int res)
        {
            int cnt=teachers.Count;
           if(teachers!=null)
            {
                var qry = "insert into tblteacher(schoolid, teachername, teachermobile ,teacheremail ,teacheraddress ," +
                    "teacherjoindate ,teachersubject,isdeleted)values(@schoolid, @teachername, @teachermobile ,@teacheremail ," +
                    "@teacheraddress ,@teacherjoindate ,@teachersubject,0)";
                using (DbConnection dbConnection=WriterConnection)
                {
                    await dbConnection.OpenAsync();
                    foreach(Teacher t in teachers)
                    {
                        t.schoolid = res;
                        var result = await dbConnection.ExecuteAsync(qry, t);
                        //return result;
                    }
                    var nofteach = await dbConnection.QuerySingleAsync<int>("select noofteacher from tblschool where schoolid=@res", new { res });
                    var updnoofteacher = await dbConnection.ExecuteAsync("update tblschool set noofteacher=(@cnt+@nofteach) where schoolid=@res", new { res,cnt,nofteach });
                }
            }
           
        }

        public  async Task<int> AddClass(List<InsertClass> cls, int sid)
        {
            int result = 0;
           if(cls.Count!=0)
            {
                using (DbConnection dbConnection = WriterConnection)
                {
                    await dbConnection.OpenAsync();
                    var qry = "insert into tblclass(classstd, classfess, schoolid ,teacherid ,createddate,isdeleted)" +
                        "values(@classstd, @classfess ,@schoolid ,@teacherid ,@createddate,0) ";
                    foreach (var c in cls)
                    {
                        c.createddate = DateTime.Now;
                        c.schoolid=sid;
                        result=await dbConnection.ExecuteAsync(qry, c);
                        result++;
                    }
                    return result - 1;

                }
            }
            return 0;
           
        }
       

        public async Task<int> DeleteSchool(int id)
        {
            var qry = "update tblschool set isdeleted=1 where schoolid=@id";
            using(DbConnection dbConnection=WriterConnection)
            {
                await dbConnection.OpenAsync();
                var result = await dbConnection.ExecuteAsync(qry, new { id });
                await dbConnection.ExecuteAsync("update tblclass set isdeleted=1 where schoolid=@id", new {id});
                await dbConnection.ExecuteAsync("update tblteacher set isdeleted=1 where schoolid=@id", new { id });
                return result;

            }
        }

        public async Task<BaseResponse> GetSchool(int pageno, int pagesize, string? schoolname)
        {
            BaseResponse baseResponse=new BaseResponse();
            PaginationModel paginationModel=new PaginationModel();
            List<School> schools = new List<School>();
            if(pageno==0)
            {
                pageno = 1;
            }
            if (pagesize == 0)
            {
                pagesize = 5;
            }
            var val = (pageno - 1) * pagesize;
            var qry = @"select * from tblschool where isdeleted=0 and (schoolname like '%'+@schoolname+'%' or schoolname is null) order by schoolid desc
                        offset @val rows fetch next @pagesize rows only;
                        select @pageno as pageno,count(distinct schoolid) as TotalPages from tblschool where isdeleted = 0";
            using(DbConnection dbConnection=ReaderConnection)
            {
                await dbConnection.OpenAsync();
               var par = new {pageno,pagesize, schoolname, val };
                var result = await dbConnection.QueryMultipleAsync(qry, par);
                var sch = await result.ReadAsync<School>();
                var pagination = await result.ReadAsync<PaginationModel>();
                schools=sch.ToList();
                paginationModel=pagination.FirstOrDefault();
                int last = 0;
                int pagecount = 0;
                last = paginationModel.Totalpages % pagesize;
                pagecount= paginationModel.Totalpages / pagesize;
                paginationModel.Pagecount = paginationModel.Totalpages;
                paginationModel.Totalpages = pagecount;
                if(last>0)
                {
                    paginationModel.Totalpages = pagecount+1;
                }
                baseResponse.ResponseData = paginationModel;
                baseResponse.ResponseData1 = schools;
                return baseResponse;
            }

        }

        public async Task<School> GetSchoolById(int id)
        {
            School school = new School();
            var qry = "select * from tblschool where schoolid=@id";
            using( DbConnection dbConnection=ReaderConnection)
            {
                await dbConnection.OpenAsync();
                 school=await dbConnection.QuerySingleAsync<School>(qry, new {id});
                var cls=await dbConnection.QueryAsync<InsertClass>("select * from tblclass where schoolid=@id", new { id = school.schoolid });
                var tech = await dbConnection.QueryAsync<Teacher>("select * from tblteacher where schoolid=@id", new { id = school.schoolid });
                school.classes = cls.ToList();
                school.teachers = tech.ToList();
            }
            return school;
        }

        public async Task<int> UpdateSchool(School school)
        {

            var qry = "update tblSchool set schoolname=@schoolname, schoolgrade=@schoolgrade" +
                ", noofteacher=@noofteacher ,schooladdress=@schooladdress , " +
                "schooltelephone=@schooltelephone ,schoolemail=@schoolemail, schooltype=@schooltype" +
                " ,schoolestablisheddate=@schoolestablisheddate where schoolid=@schoolid";
               using(DbConnection dbConnection=WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res=await dbConnection.ExecuteAsync(qry,school);
                return res;
            }
        }
    }
}
