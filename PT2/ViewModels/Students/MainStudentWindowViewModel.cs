using PT2.Data;
using PT2.Data.Interfaces;
using PT2.Data.Repositories;
using PT2.Models;
using PT2.ViewModels.Base;
using PT2.Views.Students;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PT2.ViewModels.Students
{
    class MainStudentWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Student> Students { get; set; }
        public ICollectionView FilteredStudents { get; set; }
        public Student SelectedStudent { get; set; }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                Filter();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        private IStudentRepository _studentRepository;
        private IRollCallBookRepository _rollCallBookRepository;

        public MainStudentWindowViewModel()
        {
            _studentRepository = new StudentRepository();
            _rollCallBookRepository = new RollCallBookRepository();
            LoadData();
            AddCommand = new RelayCommand(AddStudent);
            EditCommand = new RelayCommand<Student>(EditStudent);
            DeleteCommand = new RelayCommand(DeleteStudent, () => SelectedStudent != null);
            ResetCommand = new RelayCommand(ResetChoice);
        }

        private void LoadData()
        {
            Students = new ObservableCollection<Student>(_studentRepository.GetAll());
            FilteredStudents = CollectionViewSource.GetDefaultView(Students);
        }

        private void AddStudent()
        {
            var addStudentWindow = new AddStudentWindow();
            var addStudentViewModel = (AddStudentViewModel)addStudentWindow.DataContext;
            addStudentWindow.ShowDialog();

            if (addStudentViewModel.addedStudent != null)
            {
                Students.Add(addStudentViewModel.addedStudent);
            }
        }

        private void EditStudent(Student student)
        {
            var editStudentWindow = new EditStudentWindow();
            var editStudentViewModel = new EditStudentViewModel(student);
            editStudentViewModel.CloseAction = () => { editStudentWindow.Close(); };
            editStudentWindow.DataContext = editStudentViewModel;


            if (editStudentViewModel.editedStudent != null)
            {
                var oldStudent = Students.FirstOrDefault(s => s.StudentId == editStudentViewModel.editedStudent.StudentId);

                if (oldStudent != null)
                {
                    Students.Remove(oldStudent);
                    Students.Add(editStudentViewModel.editedStudent);
                }

            }
        }

        private void DeleteStudent()
        {
           var result = MessageBox.Show("Are you sure to delete cascade!", "Sure?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) {
                _studentRepository.deleteCascade(SelectedStudent);
                Students.Remove(SelectedStudent);
                SelectedStudent = null;
            }
        }

        private void ResetChoice()
        {
            SearchText = null;
            _searchText = string.Empty;
        }

        private void Filter()
        {
            FilteredStudents.Filter = (e) =>
            {
                // Use string.IsNullOrEmpty for a more robust check
                if (string.IsNullOrEmpty(SearchText))
                {
                    return true;
                }

                // Ensure the item is a Student
                if (e is Student cur)
                {
                    // The '?' before .Contains prevents a crash if the property is null.
                    // The '== true' handles the case where the result of the Contains is null.
                    bool firstNameMatch = cur.FirstName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true;
                    bool midNameMatch = cur.MidName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true;
                    bool lastNameMatch = cur.LastName?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) == true;

                    return firstNameMatch || midNameMatch || lastNameMatch;
                }

                return true; // Or false, depending on desired behavior for non-student items
            };
        }

    }
}
