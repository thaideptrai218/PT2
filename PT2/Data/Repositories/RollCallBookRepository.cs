using PT2.Data.Interfaces;
using PT2.Models;
using System.Collections.Generic;
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

        public void removeRange(List<RollCallBook> rollCallBooks)
        {
            _context.RollCallBooks.RemoveRange(rollCallBooks);
            _context.SaveChanges();
        }
    }
}
