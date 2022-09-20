using Domain.Entities.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IQuizRepository : IRepository<Quiz>
    {
        public Task<IEnumerable<Quiz>> GetAll(int personId);
        public Task<IEnumerable<Quiz>> GetAllByProfessorId(int professorId);
        public Task<Quiz> GetById(long Id,int personId);
    }
}
