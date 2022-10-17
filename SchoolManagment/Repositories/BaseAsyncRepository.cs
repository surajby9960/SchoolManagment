using System.Data.Common;
using System.Data.SqlClient;

namespace SchoolManagment.Repositories
{
    public class BaseAsyncRepository
    {
        private readonly string sqlwriterconnection;
        private readonly string sqlreaderconnection;
        public string databasetype;
        public BaseAsyncRepository(IConfiguration configuration)
        {
            sqlwriterconnection = configuration.GetSection("DBInfo:WriterConnectionString").Value;
            sqlreaderconnection = configuration.GetSection("DBInfo:ReaderConnectionString").Value;
            databasetype = configuration.GetSection("DBInfo:dbType").Value;
        }
        internal DbConnection WriterConnection
        {
            get
            {
                switch (databasetype)
                {
                    case "SqlServer":
                        return new SqlConnection(sqlwriterconnection);
                    default:
                        return new SqlConnection(sqlwriterconnection);
                }
            }
        }
        internal DbConnection ReaderConnection
        {
            get
            {
                switch (databasetype)
                {
                    case "SqlServer":
                        return new SqlConnection(sqlreaderconnection);
                    default:
                        return new SqlConnection(sqlreaderconnection);
                }
            }
        }
    }
}
