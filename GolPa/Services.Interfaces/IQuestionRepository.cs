using Domain.Entities.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IQuestionRepository : IRepository<QuestionModel>
    {
        public Task<IEnumerable<QuestionModel>> GetAll(long quizId,int personId);
        public Task<QuestionModel> Get(long questinId, int personId);
        public Task<int> InsertAnswer(QuestionAnswer entity);
        public Task<int> UpdateAnswer(QuestionAnswer entity);
        public Task<IEnumerable<QuestionAnswer>> GetAllQuestionAnswer(int qId,int personId);
        public Task<QuestionAnswer> GetQuestionAnswerById(int id);

        public Task<IEnumerable<QuizPerson>> GetAllQuizPerson(long quizId);
        public Task<QuizPerson> GetQuizPersonById(long Id);
        public Task<int> InsertQuizPerson(QuizPerson entity);
        public Task<int> UpdateQuizPerson(QuizPerson entity);
    }
}
