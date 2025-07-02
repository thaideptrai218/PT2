using PT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT2.Data.Repositories
{
    public class CourseScheduleRepository : ICourseScheduleRepository
    {
        private readonly ApContext _context = new ApContext();

        public void add(CourseSchedule courseSchedule)
        {
            _context.CourseSchedules.Add(courseSchedule);
            _context.SaveChanges();
        }

        public List<CourseSchedule> GetAll()
        {
            return _context.CourseSchedules.ToList();
        }

        public void remove(CourseSchedule courseSchedule)
        {
            
            _context.CourseSchedules.Remove(courseSchedule);
            _context.SaveChanges();
        }

        public void update(CourseSchedule courseSchedule)
        {
            _context.CourseSchedules.Update(courseSchedule);
            _context.SaveChanges();
        }
    }
}
