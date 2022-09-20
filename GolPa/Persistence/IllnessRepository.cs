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
    public class IllnessRepository : IIllnessRepository
    {
        private readonly IDbConnection _dbConnection;
        public IllnessRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("illnessId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spDeleteIllness", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<Illness> Get(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("illnessId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QuerySingleAsync<Illness>(con, "spReadIllnessById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Illness>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryAsync<Illness>(con, "spReadAllIllnesses", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(Illness entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("regUser", entity.RegUser);
            parameters.Add("description", entity.Description);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spCreateIllness", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Update(Illness entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("editUser", entity.EditUser);
            parameters.Add("illnessId", entity.IllnessId);
            parameters.Add("description", entity.Description);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spUpdateIllness", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
