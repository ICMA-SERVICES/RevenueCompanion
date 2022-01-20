using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RevenueCompanion.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace RevenueCompanion.Application.DapperServices
{
    public class Dapperr : IDapper
    {
        private readonly IConfiguration _config;
        private string constring;
        IOptions<ConnectionStrings> myconnectionString;
        public Dapperr(IConfiguration config, IOptions<ConnectionStrings> connectionString)
        {
            _config = config;
            myconnectionString = connectionString;
            constring = myconnectionString.Value.DefaultConnection;
        }
        public void Dispose()
        {

        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(constring);
            var response = db.Execute(sp, param: parms, commandType: CommandType.StoredProcedure);
            return response;
        }

        public string ExecuteString(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(constring);
            var response = db.Query(sp, param: parms, commandType: CommandType.StoredProcedure);
            return response.ToString();
        }
        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(constring);
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, string customConString, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(customConString);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(constring);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(constring);
        }

        public int Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            int result;
            using IDbConnection db = new SqlConnection(constring);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Execute(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        //public int Insert(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        //{
        //    int result;
        //    using IDbConnection db = new SqlConnection(constring);
        //    try
        //    {
        //        if (db.State == ConnectionState.Closed)
        //            db.Open();

        //        using var tran = db.BeginTransaction();
        //        try
        //        {
        //            result = db.Execute(sp, parms, commandType: commandType, transaction: tran);
        //            tran.Commit();
        //            result++;
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (db.State == ConnectionState.Open)
        //            db.Close();
        //    }

        //    return result;
        //}

        public int Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            int result;
            using IDbConnection db = new SqlConnection(constring);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Execute(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}
