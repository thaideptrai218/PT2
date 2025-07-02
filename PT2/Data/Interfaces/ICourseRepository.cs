using PT2.Models;

namespace PT2.Data.Interfaces
{
    interface ICourseRepository
    {
        List<Course> GetAll();
        void add(Course course);
        void remove(Course deletedStudent);
        void update(Course updatedStudent);
    }
}
