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
    public class QuizRepository : IQuizRepository
    {
        private readonly IDbConnection _dbConnection;

        public QuizRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("quizId", id);
            using (var con = _dbConnection.GetConnection)
                return await SqlMapper.ExecuteAsync(con, "spDeleteQuizz", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Quiz>> GetAll(int personId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("personId", personId);
            using (var con = _dbConnection.GetConnection)
                return await SqlMapper.QueryAsync<Quiz>(con, "spReadAllQuizz", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Quiz>> GetAllByProfessorId(int professorId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("professorId", professorId);
            using (var con = _dbConnection.GetConnection)
                return await SqlMapper.QueryAsync<Quiz>(con, "spReadAllProfessorQuizzes", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Quiz> GetById(long Id,int personId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("quizId", Id);
            parameters.Add("personId", personId);
            using (var con = _dbConnection.GetConnection)
                return await SqlMapper.QuerySingleOrDefaultAsync<Quiz>(con, "spReadQuizzById", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }


        public async Task<int> Insert(Quiz entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("examTitle", entity.ExamTitle);
            parameters.Add("examDate", MD.PersianDateTime.Core.PersianDateTime.Parse(entity.ExamDate).ToDateTime());
            parameters.Add("examType", entity.ExamType);
            parameters.Add("regUser", entity.RegUser);
            parameters.Add("professor", entity.Professor);
            parameters.Add("reqCourseId", entity.ReqId);
            parameters.Add("description", entity.Description);
            parameters.Add("forRet", dbType:System.Data.DbType.Int32,direction:System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await SqlMapper.ExecuteAsync(con, "spCreateQuiz", parameters, commandType: System.Data.CommandType.StoredProcedure);
                res = (res <= 0) ? parameters.Get<int>("forRet") : res;
                return res;
            }
        }

        public async Task<int> Update(Quiz entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("examTitle", entity.ExamTitle);
            parameters.Add("quizId", entity.Id);
            parameters.Add("examDate", MD.PersianDateTime.Core.PersianDateTime.Parse(entity.ExamDate).ToDateTime());
            parameters.Add("examType", entity.ExamType);
            parameters.Add("regUser", entity.EditUser);
            parameters.Add("professor", entity.Professor);
            parameters.Add("reqCourseId", entity.ReqId);
            parameters.Add("description", entity.Description);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await SqlMapper.ExecuteAsync(con, "spUpdateQuiz", parameters, commandType: System.Data.CommandType.StoredProcedure);
                res = (res <= 0) ? parameters.Get<int>("forRet") : res;
                return res;
            }
        }
    }
}
