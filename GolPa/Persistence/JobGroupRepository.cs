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
    public class JobGroupRepository : IJobGroupRepository
    {
        private readonly IDbConnection _dbConnection;
        public JobGroupRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("jobGroupId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spDeleteJobGroup", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<JobGroup>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryAsync<JobGroup>(con, "spGetAllJobGroup", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<JobGroup> GetById(long Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("jobGroupId", Id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QuerySingleAsync<JobGroup>(con, "spGetJobGroupById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(JobGroup entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("regUser", entity.RegUser);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spCreateJobGroup", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Update(JobGroup entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("jobGroupId", entity.JobGroupId);
            parameters.Add("title", entity.Title);
            parameters.Add("editUser", entity.EditUser);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spUpdateJobGroup", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
