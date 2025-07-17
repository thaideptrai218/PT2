using PT2.Data.Interfaces;
using PT2.Data.Repositories;
using PT2.Models;
using PT2.ViewModels.Base;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PT2.ViewModels.Students
{
    class AddStudentViewModel
    {
        public Student addingStudent { get; set; }
        public Student addedStudent { get; set; }
        public ICommand SaveCommand { get; set; }
        public Action CloseAction { get; set; }
        private IStudentRepository _studentRepository;

        public AddStudentViewModel()
        {
            _studentRepository = new StudentRepository();
            addingStudent = new Student();
            SaveCommand = new RelayCommand(AddNewStudent);
        }

        private void AddNewStudent()
        {
            if (string.IsNullOrEmpty(addingStudent.FirstName) || string.IsNullOrEmpty(addingStudent.LastName) || string.IsNullOrEmpty(addingStudent.Roll)) return;

            string regex = "^[A-Z]{2}\\d{6}$";
            Regex re = new Regex(regex);
            if (!re.IsMatch(addingStudent.Roll))
            {
                MessageBox.Show("RollNumber must be NNXXXXXX", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_studentRepository.checkRollNumber(addingStudent))
            {
                MessageBox.Show("RollNumber duplicated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            addingStudent.StudentId = _studentRepository.GetNextId();
            _studentRepository.add(addingStudent);
            addedStudent = addingStudent;
            CloseAction?.Invoke();
        }
    }
}
