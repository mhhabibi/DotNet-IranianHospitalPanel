using Dapper;
using Domain.Entities.Entities;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Extra;

namespace Services.ApplicationServices
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IDbConnection _dbConnection;
        public CourseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("courseId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spDeleteCourse", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<Course> Get(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("courseId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QuerySingleAsync<Course>(con, "spReadCourseById", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryAsync<Course>(con, "spReadAllCourses", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GroupCourses>> GetCoursesByGroupId(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("groupId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryAsync<GroupCourses>(con, "spReadCourseByGroupId", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(Course entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("regUser", entity.RegUser);
            parameters.Add("forRet", dbType:System.Data.DbType.Int32,direction:System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spCreateCourse", parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (res == 0)
                    res = parameters.Get<int>("forRet");

                return res;
            }
        }

        public async Task<int> Update(Course entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("editUser", entity.EditUser);
            parameters.Add("courseId", entity.CourseId);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spUpdateCourse", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }


    }
}
