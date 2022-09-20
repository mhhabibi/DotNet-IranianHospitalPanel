using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IAttributeRepository
    {
        public bool CheckPermission(int permssionId, int roleId);
    }
}
