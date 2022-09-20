using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
//using System.Data;
using System.Threading.Tasks;

namespace Services.ApplicationServices
{
    public class EducationCoursesRepository : IEducationCoursesRepository
    {
        private readonly IDbConnection _dbConnection;
        public EducationCoursesRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public Task<int> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<EducationCourse> Get(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("educationCurseId", id);
            using(var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryFirstOrDefaultAsync<EducationCourse>(con, "spGetEducationCurseById", parameters, commandType:System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<EducationCourse>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                return await SqlMapper.QueryAsync<EducationCourse>(con, "spGetEducationCurseById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Task<int> Insert(EducationCourse entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(EducationCourse entity)
        {
            throw new NotImplementedException();
        }
    }
}
