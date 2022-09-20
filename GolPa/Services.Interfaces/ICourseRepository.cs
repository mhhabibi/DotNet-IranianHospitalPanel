using Domain.Entities.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels.Extra;

namespace Services.Interfaces
{
    public interface ICourseRepository : IRepository<Course>
    {
        public Task<IEnumerable<Course>> GetAll();
        public Task<IEnumerable<GroupCourses>> GetCoursesByGroupId(int id);
        public Task<Course> Get(long id);
    }
}
