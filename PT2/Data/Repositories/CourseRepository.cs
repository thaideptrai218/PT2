using PT2.Data.Interfaces;
using PT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT2.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApContext _context = new ApContext();

        public void add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public void remove(Course deletedCourse)
        {
            _context.Courses.Remove(deletedCourse);
            _context.SaveChanges();
        }

        public void update(Course updatedCourse)
        {
            _context.Courses.Update(updatedCourse);
            _context.SaveChanges();
        }
    }
}
