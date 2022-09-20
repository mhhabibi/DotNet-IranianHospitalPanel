using Domain.Entities.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IIllnessRepository : IRepository<Illness>
    {
        public Task<IEnumerable<Illness>> GetAll();
        public Task<Illness> Get(long id);
    }
}
