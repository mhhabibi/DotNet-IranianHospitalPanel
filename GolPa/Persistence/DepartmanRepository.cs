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
    public class DepartmanRepository : IDepartmanRepository
    {
        private readonly IDbConnection _dbConnection;
        public DepartmanRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("departmanId", id);
            using (var con = _dbConnection.GetConnection)
            {
                var res = await SqlMapper.ExecuteAsync(con, "spDeleteDepartman", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        public async Task<IEnumerable<DepartmanViewModel>> GetAll()
        {
            DynamicParameters parameters = new DynamicParameters();
            using (var con = _dbConnection.GetConnection)
            {
                var departmans = await SqlMapper.QueryAsync<DepartmanViewModel, ForInPerson, DepartmanViewModel>(con, "spGetAllDepartman",
                    (departmanViewModel, forInPerson) =>
                    {
                        departmanViewModel.Manager = forInPerson;
                        return departmanViewModel;
                    },
                    splitOn: "PersonId", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);

                return departmans;
            }
        }

        public async Task<DepartmanViewModel> GetById(long Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("departmanId", Id);
            using (var con = _dbConnection.GetConnection)
            {
                var departmans = await SqlMapper.QueryAsync<DepartmanViewModel, ForInPerson, DepartmanViewModel>(con, "spGetDepartmanById",
                    (departmanViewModel, forInPerson) =>
                    {
                        departmanViewModel.Manager = forInPerson;
                        return departmanViewModel;
                    },
                    splitOn: "PersonId", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);
                return departmans.First();
            }
        }

        public async Task<IEnumerable<dynamic>> GetDepartmanCourses(int departmanId)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("departmanId", departmanId);
            using (var con = _dbConnection.GetConnection)
            {
                return await SqlMapper.QueryAsync<dynamic>(con, "spReadAllDepartmanCourses", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(Departman entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("regUser", Int32.Parse(entity.RegUser));
            parameters.Add("manager", entity.Manager);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);

            using (var con = _dbConnection.GetConnection)
            {
                var res = await SqlMapper.ExecuteAsync(con, "spCreateDepartman", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);
                res = parameters.Get<int>("forRet");
                return res;
            }
        }

        public async Task<int> Update(Departman entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", entity.Title);
            parameters.Add("manager", entity.Manager);
            parameters.Add("departmanId", entity.DepartmanId);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            parameters.Add("editUser", entity.EditUser);

            using (var con = _dbConnection.GetConnection)
            {
                var res = await SqlMapper.ExecuteAsync(con, "spUpdateDepartman", param: parameters
                    , commandType: System.Data.CommandType.StoredProcedure);
                if (res == 0)
                    res = parameters.Get<int>("forRet");
                return res;
            }
        }
    }
}
