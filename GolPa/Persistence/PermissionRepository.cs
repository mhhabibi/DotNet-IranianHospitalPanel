using Dapper;
using Domain.Entities.Entities;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IDbConnection _dbConnection;
        public PermissionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Permission> Get(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("permissionId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QuerySingleAsync<Permission>(con, "spReadPermission", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Permission>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryAsync<Permission>(con, "spReadAllPermissions", param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
