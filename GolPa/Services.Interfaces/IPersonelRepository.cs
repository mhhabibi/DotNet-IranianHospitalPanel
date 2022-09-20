using Domain.Entities.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Extra;

namespace Services.Interfaces
{
    public interface IPersonelRepository
    {
        public Task<int> Insert(UserPost entity);
        public Task<int> Update(UserPost entity);
        public Task<IEnumerable<UserViewModel>> GetAll();
        public Task<UserViewModel> Get(long id);
        public Task<IEnumerable<dynamic>> GetDepartmansPersonel(int applicantId);
        public Task<IEnumerable<dynamic>> GetDepartmanManager();
        public Task<IEnumerable<ProfessorCourses>> GetProfessorByCourseId(long id);
        public Task<int> Delete(long id);
    }
}
