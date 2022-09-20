using Domain.Entities.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IJobRepository : IRepository<Job>
    {
        public Task<IEnumerable<JobViewModel>> GetAll();
        public Task<JobViewModel> GetById(long Id);
    }
}
