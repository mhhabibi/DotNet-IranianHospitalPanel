using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUserRepository
    {
        public User Authenticate(string username, string password);
        public IEnumerable<User> GetAll();
        public User GetById(int id);
        public bool CheckPermission(string permission, int personId);
    }
}
