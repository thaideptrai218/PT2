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
        List<Student> GetAllWithStudentObject();
        void add(Student student);
        void remove(Student student);
        void update(Student updatedStudent);
        int GetNextId();
        bool checkRollNumber(Student s);
        Student getByID(int id);

        void deleteCascade(Student student);
    }
}
