using PT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT2.Data.Interfaces
{
    interface IStudentRepository
    {
        List<Student> GetAll();
        void add(Student student);
        void remove(Student student);
        void update(Student updatedStudent);
    }
}
