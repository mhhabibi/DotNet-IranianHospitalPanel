using Dapper;
using Domain.Entities.Entities;
using Domain.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices
{
    public class EducationalCalendarRepository : IEducationalCalendarRepository
    {
        private readonly IDbConnection _dbConnection;
        public EducationalCalendarRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<EducationalCalendar>> GetEducationCalender(bool hasPermission,int regUser)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("hasPermission", hasPermission);
            parameters.Add("regUser", regUser);
            using (var con = _dbConnection.GetConnection)
                return await Dapper.SqlMapper.QueryAsync<EducationalCalendar>(con, "spReadEducationalCalender", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<EducationalCalendar>> GetEducationCalenderByDate(bool hasPermission, int regUser, string fromDate, string toDate)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("hasPermission", hasPermission);
            parameters.Add("regUser", regUser);
            parameters.Add("fromDate", MD.PersianDateTime.Core.PersianDateTime.Parse(fromDate).ToDateTime());
            parameters.Add("toDate", MD.PersianDateTime.Core.PersianDateTime.Parse(toDate).ToDateTime());
            using (var con = _dbConnection.GetConnection)
                return await Dapper.SqlMapper.QueryAsync<EducationalCalendar>(con, "spReadEducationalCalenderByDate", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
