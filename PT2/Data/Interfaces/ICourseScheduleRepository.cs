using PT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT2.Data
{
    interface ICourseScheduleRepository
    {
        List<CourseSchedule> GetAll();
        void add(CourseSchedule courseSchedule);
        void remove(CourseSchedule courseSchedule);
        void update(CourseSchedule courseSchedule);
    }
}
