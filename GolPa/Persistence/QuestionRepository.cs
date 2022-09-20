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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDbConnection _dbConnection;

        public QuestionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> Delete(long id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("questionId", id);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.ExecuteAsync(con, "spDeleteQuestion", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<QuestionModel>> GetAll(long quizId,int personId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("quizId", quizId);
            parameters.Add("personId", personId);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QueryAsync<QuestionModel>(con, "spReadAllQuestion", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<QuestionModel> Get(long id,int personId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("questionId", id);
            parameters.Add("personId", personId);
            using (var con = _dbConnection.GetConnection)
            {
                return await Dapper.SqlMapper.QuerySingleOrDefaultAsync<QuestionModel>(con, "spReadPermission", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(QuestionModel entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("question",entity.Question);
            parameters.Add("option1",entity.Option1);
            parameters.Add("option2",entity.Option2);
            parameters.Add("option3",entity.Option3);
            parameters.Add("option4",entity.Option4);
            parameters.Add("keyAnswer", entity.KeyAnswer);
            parameters.Add("quizId", entity.QuizeId);
            parameters.Add("forRet", dbType:System.Data.DbType.Int32,direction:System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spCreateQuestion", parameters, commandType: System.Data.CommandType.StoredProcedure);
                res = (res == 0) ? parameters.Get<int>("forRet"):res;
                return res;
            }
        }

        public async Task<int> Update(QuestionModel entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("question", entity.Question);
            parameters.Add("questionId", entity.Id);
            parameters.Add("option1", entity.Option1);
            parameters.Add("option2", entity.Option2);
            parameters.Add("option3", entity.Option3);
            parameters.Add("option4", entity.Option4);
            parameters.Add("keyAnswer", entity.KeyAnswer);
            parameters.Add("quizId", entity.QuizeId);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spUpdateQuestion", parameters, commandType: System.Data.CommandType.StoredProcedure);
                res = (res == 0) ? parameters.Get<int>("forRet") : res;
                return res;
            }
        }

        public async Task<int> InsertAnswer(QuestionAnswer entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("qId", entity.QId);
            parameters.Add("answerId", entity.AnswerId);
            parameters.Add("personId", entity.PersonId);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spCreatePersonAnswer", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        public async Task<int> UpdateAnswer(QuestionAnswer entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", entity.Id);
            parameters.Add("answerId", entity.AnswerId);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spUpdatePersonAnswer", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllQuestionAnswer(int qId, int personId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("qId", qId);
            parameters.Add("personId", personId);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                var res = await Dapper.SqlMapper.QueryAsync<QuestionAnswer>(con, "spReadAllPersonAnswer", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        public async Task<QuestionAnswer> GetQuestionAnswerById(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                var res = await Dapper.SqlMapper.QuerySingleAsync<QuestionAnswer>(con, "spReadPersonAnswer", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }


        #region QuizPerson
        public async Task<IEnumerable<QuizPerson>> GetAllQuizPerson(long Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("quizId", Id);
            using (var con = _dbConnection.GetConnection)
            {
                var res = await Dapper.SqlMapper.QueryAsync<QuizPerson>(con, "spReadAllQuizPersonByQuizId", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        public async Task<QuizPerson> GetQuizPersonById(long Id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", Id);
            using (var con = _dbConnection.GetConnection)
            {
                var res = await Dapper.SqlMapper.QuerySingleAsync<QuizPerson>(con, "spReadAllQuizPersonById", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return res;
            }
        }

        public async Task<int> InsertQuizPerson(QuizPerson entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("grade", entity.Grade);
            parameters.Add("quizId", entity.QuizId);
            parameters.Add("personId", entity.PersonId);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spCreateQuizPerson", parameters, commandType: System.Data.CommandType.StoredProcedure);
                res = (res == 0) ? parameters.Get<int>("forRet") : res;
                return res;
            }
        }

        public async Task<int> UpdateQuizPerson(QuizPerson entity)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("grade", entity.Grade);
            parameters.Add("Id", entity.Id);
            parameters.Add("quizId", entity.QuizId);
            parameters.Add("personId", entity.PersonId);
            parameters.Add("forRet", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            using (var con = _dbConnection.GetConnection)
            {
                int res = await Dapper.SqlMapper.ExecuteAsync(con, "spUpdateQuizPerson", parameters, commandType: System.Data.CommandType.StoredProcedure);
                res = (res==0) ? parameters.Get<int>("forRet"):res;
                return res;
            }
        }
        #endregion
    }
}
