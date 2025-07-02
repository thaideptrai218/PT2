using PT2.Models;

namespace PT2.Data.Interfaces
{
    interface IRoomRepository
    {
        List<Room> GetAll();
        void add(Room room);
        void remove(Room room);
        void update(Room room);
    }
}
