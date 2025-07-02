using PT2.Data.Interfaces;
using PT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT2.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApContext _context = new ApContext();

        public void add(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public List<Room> GetAll()
        {
            return _context.Rooms.ToList();
        }

        public void remove(Room room)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }

        public void update(Room room)
        {
            _context.Rooms.Update(room);
            _context.SaveChanges();
        }
    }
}
