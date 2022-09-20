using Domain.Entities.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IRequestCourseRepository : IRepository<ReqCourse>
    {
        public Task<int> AcceptCourse(AcceptCourse acceptCourse);
        public Task<IEnumerable<RequestCourseViewModel>> GetAll(bool hasPermission,int curUser);
        public Task<IEnumerable<RequestCourseViewModel>> GetAllFinishedRequestCourses(int roleId,int curUser);
        public Task<IEnumerable<RequestCourseViewModel>> GetAllPerformingRequestCourses(int roleId,int curUser);
        public Task<IEnumerable<RequestCourseViewModel>> GetAllNotConfirmedRequestCourses();
        public Task<RequestCourseViewModel> Get(int curUser,int reqId);
        public Task<IEnumerable<AttachFile>> GetAllAttach(int reqCourseId);
        public Task<int> AddAttach(string fileName, IFormFile file,int reqCourseId, int regUser);
        public Task<int> DeleteAttach(int reqCourseId);
    }
}
