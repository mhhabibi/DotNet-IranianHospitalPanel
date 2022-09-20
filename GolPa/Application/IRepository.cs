using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        public Task<int> Insert(T entity);
        public Task<int> Update(T entity);
        public Task<int> Delete(long id);
    }
}
