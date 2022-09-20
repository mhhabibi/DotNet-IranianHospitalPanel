using Domain.Entities.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        public Task<IEnumerable<Patient>> GetAll();
        public Task<Patient> GetById(long id);
    }
}
