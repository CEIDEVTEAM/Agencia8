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
        private bool flagTranConn = false;

        public DapperService(IOptions<DatabaseSettings> config)
        {
            _config = config;
        }

        public void Dispose()
        {
            conn.Close();
        }

        public DbConnection GetDbconnection(bool flagTran = false)
        {
            if (flagTran)
            {
                conn = new SqlConnection(_config.Value.ConnectionStringTransactional);
                flagTranConn = true;
            }
            else
                conn = new SqlConnection(_config.Value.ConnectionString);

            return conn;
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            if (flagTranConn)
            {
                using (IDbConnection db = new SqlConnection(_config.Value.ConnectionStringTransactional))
                {
                    db.Open();
                    return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
                }
            }
            else
            {
                using (IDbConnection db = new SqlConnection(_config.Value.ConnectionString))
                {
                    db.Open();
                    return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
                }
            }
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            if (flagTranConn)
            {
                using (IDbConnection db = new SqlConnection(_config.Value.ConnectionStringTransactional))
                {
                    db.Open();
                    return db.Query<T>(sp, parms, commandType: commandType).ToList();
                }
            }
            else
            {
                using (IDbConnection db = new SqlConnection(_config.Value.ConnectionString))
                {
                    db.Open();
                    return db.Query<T>(sp, parms, commandType: commandType).ToList();
                }
            }
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;

            if (flagTranConn)
            {
                using (IDbConnection db = new SqlConnection(_config.Value.ConnectionStringTransactional))
                {
                    db.Open();

                    using (var tran = db.BeginTransaction())
                    {
                        result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                        tran.Commit();
                    }
                }
            }
            else
            {
                using (IDbConnection db = new SqlConnection(_config.Value.ConnectionString))
                {
                    db.Open();

                    using (var tran = db.BeginTransaction())
                    {
                        result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                        tran.Commit();
                    }
                }
            }

            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;

            if (flagTranConn)
            {
                using (IDbConnection db = new SqlConnection(_config.Value.ConnectionStringTransactional))
                {
                    db.Open();

                    using (var tran = db.BeginTransaction())
                    {
                        result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                        tran.Commit();
                    }
                }
            }
            else
            {
                using (IDbConnection db = new SqlConnection(_config.Value.ConnectionString))
                {
                    db.Open();

                    using (var tran = db.BeginTransaction())
                    {
                        result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                        tran.Commit();
                    }
                }
            }

            return result;
        }

    }
}
