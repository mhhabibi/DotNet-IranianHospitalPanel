using Domain.Entities.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IDepartmanRepository : IRepository<Departman>
    {
        public Task<IEnumerable<DepartmanViewModel>> GetAll();
        public Task<IEnumerable<dynamic>> GetDepartmanCourses(int departmanId);
        public Task<DepartmanViewModel> GetById(long Id);
    }
}
