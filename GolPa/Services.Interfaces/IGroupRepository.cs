using Domain.Entities.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        public Task<IEnumerable<GroupViewModel>> GetAll();
        public Task<GroupViewModel> Get(long id);
    }
}
