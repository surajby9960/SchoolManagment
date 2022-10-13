using SchoolManagment.Model;
using SchoolManagment.Repositories.Interfaces;
using System.Data.Common;
using Dapper;

namespace SchoolManagment.Repositories
{
    public class SportsRepository : BaseAsyncRepository, ISportsRepository
    {
        public SportsRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AddSport(Sports sports)
        {

            var qry = "insert into tblsports(sportname,isdeleted)values(@sportname,@isdeleted)";
            using(DbConnection dbConnection=WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, sports);
                return res;
            }
        }

        public  async Task<int> DeleteSport(int id)
        {
            var qry = "update tblsports set isdeleted=@isdeleted where sportsid=@id";
            using (DbConnection dbConnection = WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry, new {id});
                return res;
            }
        }

        public async Task<IEnumerable<Sports>> GetAllSport()
        {
            var qry = "select * from tblsports";
            using (DbConnection dbConnection = WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.QueryAsync<Sports>(qry);
                return res.ToList();
            }
        }

        public async Task<Sports> GetSportById(int id)
        {

            var qry = "select * from tblsports where sportid=@id";
            using (DbConnection dbConnection = WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.QuerySingleAsync<Sports>(qry, new {id});
                return res;
            }
        }

        public async Task<int> UpdateSport(Sports sports)
        {
            var qry = "update tblsports set sportsname=@sportsname where sportsid=@id";
            using (DbConnection dbConnection = WriterConnection)
            {
                await dbConnection.OpenAsync();
                var res = await dbConnection.ExecuteAsync(qry,sports);
                return res;
            }
        }
    }
}
