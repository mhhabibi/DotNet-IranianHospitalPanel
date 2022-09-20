using Domain.Entities.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IJobGroupRepository:IRepository<JobGroup>
    {
        public Task<IEnumerable<JobGroup>> GetAll();
        public Task<JobGroup> GetById(long Id);
    }
}
