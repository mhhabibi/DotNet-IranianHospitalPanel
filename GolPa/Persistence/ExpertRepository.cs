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
    public class ExpertRepository : IExpertRepository
    {
        private readonly IDbConnection _dbConnection;
        public ExpertRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("expertId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spDeleteExpert", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<Expert> Get(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("expertId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QuerySingleOrDefaultAsync<Expert>(con, "spReadExpertById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Expert>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryAsync<Expert>(con, "spReadAllExperts", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(Expert entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("regUser", entity.RegUser);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spCreateExpert", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Update(Expert entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("editUser", entity.EditUser);
            parameters.Add("expertId", entity.ExpertId);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spUpdateExpert", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
