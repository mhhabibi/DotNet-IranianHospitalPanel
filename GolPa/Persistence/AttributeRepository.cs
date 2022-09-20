using Dapper;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ApplicationServices
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly IDbConnection _dbConnection;

        public AttributeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public bool CheckPermission(int permssionId, int roleId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("permissionId", permssionId);
            parameters.Add("roleId", roleId);
            using(var con = _dbConnection.GetConnection)
            {
                var res = SqlMapper.QuerySingleAsync(con, "spCheckPermission", parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (res == null)
                    return false;

                return true;
            }
        }
    }
}
