using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace RevenueCompanion.Application.DapperServices
{
    public interface IDapper : IDisposable
    {
        DbConnection GetDbconnection();
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, string customConString, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        string ExecuteString(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        int Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        int Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
