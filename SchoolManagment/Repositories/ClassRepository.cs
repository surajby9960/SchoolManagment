using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;
using System.Data.Common;
using Dapper;

namespace SchoolManagment.Repositories
{
    public class ClassRepository : BaseAsyncRepository ,IClassRepository
    {
        public ClassRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddClass(InsertClass cls)
        {
            var qry = "Insert into tblclass(classstd, classfess, schoolid, teacherid ,createddate,isdeleted)values" +
                "(@classstd ,@classfess, @schoolid, @teacherid ,@createddate ,0);"+
                "select cast(scope_identity() as int)";
            using (DbConnection dbConnection = WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res=await dbConnection.QueryFirstOrDefaultAsync<int>(qry,cls);
                await AddStudent(cls.students, res);
                return res;
            }
           
        }

        private async Task AddStudent(List<Student>? students, int res)
        {
            if(students.Count> 0)
            {
                var qry = "insert into tblstudent(studname, studdob, studaddress, studgender, admissiondate, sportid, classid,isdeleted )" +
                    "values(@studname ,@studdob ,@studaddress, @studgender, @admissiondate ,@sportid, @classid ,0)";
                using(DbConnection dbConnection=WriterConnection)
                {
                    await dbConnection.OpenAsync();
                    foreach(Student student in students)
                    {
                        student.admissiondate = DateTime.Now;
                        student.classid = res;
                        var result=await dbConnection.ExecuteAsync(qry,student);
                    }
                }
            }
        }

        public async Task<int> DeleteClass(int cid,int sid)
        {
            var qry = "update tblclass set isdeleted=1  where classid=@cid and schoolid=@sid";
            using(DbConnection dbConnection=WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, new { cid,sid });
                await dbConnection.ExecuteAsync("Update tblstudent set isdeleted=1 where classid=@cid", new {cid});
                return res;
                
            }
        }

        public async Task<IEnumerable<InsertClass>> GetAllClasses()
        {
            List<InsertClass> cls1 = new List<InsertClass>();
           
            var qry = "select  * from tblclass";
            using (DbConnection dbConnection = WriterConnection)
            {
                await dbConnection.OpenAsync();
               var cls = await dbConnection.QueryAsync<InsertClass>(qry);
                cls1= cls.ToList();
                foreach (InsertClass c2 in cls1)
                {

                    var students = await dbConnection.QueryAsync<Student>("select * from tblstudent where classid=@id", new { id = c2.classid });
                    c2.students = students.ToList();
                    
                }
               
                return cls1;
            }
                        
        }

        public async Task<UpdateClass> GetClassById(int id)
        {
            var qry = "select * from tblclass where classid=@id";
            UpdateClass cls = new UpdateClass();
            using (DbConnection dbConnection = ReaderConnection)
            {
                await dbConnection.OpenAsync();
                cls = await dbConnection.QuerySingleAsync<UpdateClass>(qry, new { id });
                var stud = await dbConnection.QueryAsync<Student>("select * from tblstudent where classid=@id", new { id = cls.classid });
                cls.students = stud.ToList();
            }
            return cls;
        }

        public async Task<int> UpdateClass(UpdateClass cls)
        {
            var qry = "update tblclass set classstd=@classstd ,classfess=@classfess ,schoolid=@schoolid ,teacherid=@teacherid " +
                 ",modifydate=@modifydate where classid=@classid ";
            using(DbConnection dbConnection=WriterConnection)
            {
                await dbConnection.OpenAsync();
                cls.modifydate=DateTime.Now;
                var res = await dbConnection.ExecuteAsync(qry, cls);
                return res;
            }
        }
    }
}
