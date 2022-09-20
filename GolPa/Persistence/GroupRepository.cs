using Dapper;
using Domain.Entities.Entities;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Extra;

namespace Services.ApplicationServices
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IDbConnection _dbConnection;
        public GroupRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("groupId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spDeleteGroup", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<GroupViewModel> Get(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("groupId", id);
            using (var con = _dbConnection.GetConnection)
            {
                var groupCourses = await Dapper.SqlMapper.QueryAsync<GroupCourses>(con, "spReadAllGroupCourses", commandType: System.Data.CommandType.StoredProcedure);
                var group = await Dapper.SqlMapper.QuerySingleAsync<GroupViewModel>(con, "spReadGroupById", parameters, commandType: System.Data.CommandType.StoredProcedure);
                group.groupCourses = groupCourses.Where(x => x.GroupId == group.GroupId).Select(x=>x).ToList();
                return group;
            }
        }

        public async Task<IEnumerable<GroupViewModel>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using(var con = _dbConnection.GetConnection)
            {
                var groupCourses = await Dapper.SqlMapper.QueryAsync<GroupCourses>(con, "spReadAllGroupCourses", parameters, commandType: System.Data.CommandType.StoredProcedure);
                var groups = await Dapper.SqlMapper.QueryAsync<GroupViewModel>(con, "spReadAllGroups", parameters, commandType: System.Data.CommandType.StoredProcedure);
                foreach (var group in groups)
                    group.groupCourses = groupCourses.Where(x=>x.GroupId==group.GroupId).Select(x => x).ToList();

                return groups;
            }
        }

        public async Task<int> Insert(Group entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("groupCourses", JsonSerializer.Serialize(entity.GroupCourses));
            parameters.Add("regUser", entity.RegUser);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spCreateGroup", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Update(Group entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("editUser", entity.EditUser);
            parameters.Add("groupCourses", JsonSerializer.Serialize(entity.GroupCourses));
            parameters.Add("groupId", entity.GroupId);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spUpdateGroup", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
