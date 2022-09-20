using Dapper;
using Domain.Entities.Entities;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.ForIn;

namespace Services.ApplicationServices
{
    public class JobRepository : IJobRepository
    {
        private readonly IDbConnection _dbConnection;
        public JobRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("jobId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spDeleteJob", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<JobViewModel>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryAsync<JobViewModel,ForInJobGroup, JobViewModel>(con, "spGetAllJob", 
                    (job, forInJobGroup) =>
                {
                    job.jobGroup = forInJobGroup;
                    return job;
                }, splitOn: "JobGroupId", param:parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<JobViewModel> GetById(long Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("jobId", Id);
            using (var con = _dbConnection.GetConnection)
            {
                var jobs = await Dapper.SqlMapper.QueryAsync<JobViewModel, ForInJobGroup, JobViewModel>(con, "spGetJobById",
                    (job, forInJobGroup) =>
                    {
                        job.jobGroup = forInJobGroup;
                        return job;
                    }, splitOn: "JobGroupId", param: parameters, commandType: System.Data.CommandType.StoredProcedure);

                return jobs.First();
            }
        }

        public async Task<int> Insert(Job entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("jobGroupId", entity.JobGroupId);
            parameters.Add("regUser", entity.RegUser);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spCreateJob", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Update(Job entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("jobGroupId", entity.JobGroupId);
            parameters.Add("editUser", entity.EditUser);
            parameters.Add("jobId", entity.JobId);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spUpdateJob", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
