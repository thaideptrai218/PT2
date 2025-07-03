using Microsoft.EntityFrameworkCore;
using PT2.Data.Interfaces;
using PT2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public int GetNextId()
        {
            if (!_context.Students.Any())
            {
                return 1;
            }
            else
            {
                return _context.Students.Max(s => s.StudentId) + 1;
            }
        }

        public bool checkRollNumber(Student student)
        {
            return _context.Students.Any(s => s.Roll.Equals(student.Roll));
        }

        public Student getByID(int id)
        {
            return _context.Students.FirstOrDefault(s => s.StudentId.Equals(id));
        }

        public void deleteCascade(Student student)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var studentId = student.StudentId;

                    // Step 1: Use Raw SQL to forcefully delete from the join table.
                    // This is the most reliable way to handle problematic many-to-many deletions.
                    // NOTE: The table name 'STUDENT_COURSE' must be exact.
                    _context.Database.ExecuteSqlRaw("DELETE FROM STUDENT_COURSE WHERE StudentId = {0}", studentId);

                    // Step 2: Now handle the one-to-many relationship with RollCallBooks.
                    var rollCallBooks = _context.RollCallBooks
                                                .Where(rcb => rcb.StudentId == studentId)
                                                .ToList();
                    if (rollCallBooks.Any())
                    {
                        _context.RollCallBooks.RemoveRange(rollCallBooks);
                    }

                    // Step 3: Finally, delete the student.
                    // We use Find to attach the entity to the context before removing.
                    var studentToDelete = _context.Students.Find(studentId);
                    if (studentToDelete != null)
                    {
                        _context.Students.Remove(studentToDelete);
                    }

                    // Step 4: Save all changes made via the DbContext.
                    _context.SaveChanges();

                    // Step 5: Commit the transaction.
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApplicationException("An error occurred during the cascade delete.", ex);
                }
            }
        }

        public List<Student> GetAllWithStudentObject()
        {
            throw new NotImplementedException();
        }
    }
}
