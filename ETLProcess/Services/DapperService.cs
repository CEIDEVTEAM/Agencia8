using Dapper;
using ETLProcess.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using ETLProcess.Configuration;

namespace ETLProcess.Services
{
    public class DapperService : IDapper
    {
        private readonly IOptions<DatabaseSettings> _config;
        private SqlConnection conn;

        public DapperService(IOptions<DatabaseSettings> config)
        {
            _config = config;
        }

        public void Dispose()
        {
            conn.Close();
        }

        public DbConnection GetDbconnection()
        {
            conn = new SqlConnection(_config.Value.ConnectionString);
            return conn;
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using (IDbConnection db = new SqlConnection(_config.Value.ConnectionString))
            {
                db.Open();
                return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
            }
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection db = new SqlConnection(_config.Value.ConnectionString))
            {
                db.Open();
                return db.Query<T>(sp, parms, commandType: commandType).ToList();
            }
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;

            using (IDbConnection db = new SqlConnection(_config.Value.ConnectionString))
            {
                db.Open();

                using (var tran = db.BeginTransaction())
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
            }

            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using (IDbConnection db = new SqlConnection(_config.Value.ConnectionString))
            {
                db.Open();

                using (var tran = db.BeginTransaction())
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
            }

            return result;
        }

    }
}
