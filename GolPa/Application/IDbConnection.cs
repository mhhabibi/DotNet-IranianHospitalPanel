using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Domain.Interfaces
{
    public interface IDbConnection
    {
        public SqlConnection GetConnection { get; }

    }
}
