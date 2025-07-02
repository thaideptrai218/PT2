using PT2.Data.Interfaces;
using PT2.Data.Repositories;
using PT2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PT2.ViewModels.Students
{
    class EditStudentViewModel
    {

        public Student editingStudent { get; set; }
        public Student editedStudent { get; set; }
        public ICommand SaveCommand { get; set; }
        public Action CloseAction { get; set; }
        private IStudentRepository _studentRepository;
        private string previousRoll;


        public EditStudentViewModel(Student targetStudent)
        {
            _studentRepository = new StudentRepository();
            editingStudent = targetStudent;
            previousRoll = targetStudent.Roll;
            SaveCommand = new RelayCommand(UpdateStudent);
        }

        private void UpdateStudent()
        {
            if (string.IsNullOrEmpty(editingStudent.FirstName) || string.IsNullOrEmpty(editingStudent.LastName) || string.IsNullOrEmpty(editingStudent.Roll))
            {
                MessageBox.Show("Name and Roll must not empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ;

            string regex = "([A-Z]{2}\\d{6})";
            Regex re = new Regex(regex);
            if (!re.IsMatch(editingStudent.Roll))
            {
                MessageBox.Show("RollNumber must be NNXXXXXX", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!previousRoll.Equals(editingStudent.Roll) && _studentRepository.checkRollNumber(editingStudent))
            {
                MessageBox.Show("RollNumber duplicated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _studentRepository.update(editingStudent);
            editedStudent = editingStudent;
            CloseAction?.Invoke();
        }
    }
}
