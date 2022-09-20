using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPermissionRepository
    {
        public Task<IEnumerable<Permission>> GetAll();
        public Task<Permission> Get(long id);
    }
}
