using PT2.Data.Interfaces;
using PT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT2.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApContext _context = new ApContext();

        public void add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public void remove(Student student)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }

        public void update(Student updatedStudent)
        {
            _context.Students.Update(updatedStudent);
            _context.SaveChanges();
        }
    }
}
