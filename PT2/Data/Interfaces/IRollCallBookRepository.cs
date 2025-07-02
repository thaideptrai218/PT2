using PT2.Models;
using System.Collections.Generic;

namespace PT2.Data.Interfaces
{
    public interface IRollCallBookRepository
    {
        List<RollCallBook> GetByScheduleId(int scheduleId);

        List<RollCallBook> GetByStudentId(int studentID);
        void removeRange(List<RollCallBook> rollCallBooks);
    }
}
