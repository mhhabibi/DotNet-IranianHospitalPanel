using Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEducationalCalendarRepository
    {
        public Task<IEnumerable<EducationalCalendar>> GetEducationCalender(bool hasPermission,int regUser);
        public Task<IEnumerable<EducationalCalendar>> GetEducationCalenderByDate(bool hasPermission,int regUser,string fromDate,string toDate);
    }
}
