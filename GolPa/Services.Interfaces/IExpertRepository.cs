using Domain.Entities.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IExpertRepository : IRepository<Expert>
    {
        public Task<IEnumerable<Expert>> GetAll();
        public Task<Expert> Get(long id);
    }
}
