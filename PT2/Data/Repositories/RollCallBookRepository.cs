using PT2.Data.Interfaces;
using PT2.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PT2.Data.Repositories
{
    public class RollCallBookRepository : IRollCallBookRepository
    {
        private readonly ApContext _context = new ApContext();

        public List<RollCallBook> GetByScheduleId(int scheduleId)
        {
            return _context.RollCallBooks.Where(rcb => rcb.TeachingScheduleId == scheduleId).ToList();
        }

        public List<RollCallBook> GetByStudentId(int studentID)
        {
            return _context.RollCallBooks.Where(rcb => rcb.StudentId == studentID).ToList();
        }

        public void removeRange(List<RollCallBook> rollCallBooks)
        {
            _context.RollCallBooks.RemoveRange(rollCallBooks);
            _context.SaveChanges();
        }

        public List<RollCallBook> GetAll()
        {
            return _context.RollCallBooks.Include(rcb => rcb.Student).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
