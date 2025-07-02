using PT2.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using PT2.Models;
using System.Windows.Input;
using PT2.Data;
using PT2.Data.Repositories;
using PT2.Data.Interfaces;
using System.ComponentModel;
using PT2.Views.CourseSchedules;
using System.Windows;

namespace PT2.ViewModels.CourseSchedules
{
    class MainCourseScheduleViewModel : ViewModelBase
    {
        //DATA BINDING PROPS

        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<CourseSchedule> CourseSchedules { get; set; }
        public ICollectionView FilteredCourseSchedules { get; set; }

        private CourseSchedule _selectedCourseSchedule;
        public CourseSchedule SelectedCourseSchedule
        {
            get => _selectedCourseSchedule;
            set
            {
                _selectedCourseSchedule = value;
                OnPropertyChanged();
            }
        }

        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
                Filter();
            }
        }

        public CourseSchedule SelectedRecord { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        private ICourseScheduleRepository _courseScheduleRepository;
        private ICourseRepository _courseRepository;
        private IRollCallBookRepository _rollCallBookRepository;

        private void LoadData()
        {
            Courses = new ObservableCollection<Course>(_courseRepository.GetAll());
            CourseSchedules = new ObservableCollection<CourseSchedule>(_courseScheduleRepository.GetAll());
            FilteredCourseSchedules = CollectionViewSource.GetDefaultView(CourseSchedules);
        }


        //CONSTRUCTOR
        public MainCourseScheduleViewModel()
        {
            _courseScheduleRepository = new CourseScheduleRepository();
            _courseRepository = new CourseRepository();
            _rollCallBookRepository = new RollCallBookRepository();
            LoadData();
            AddCommand = new RelayCommand(Add);
            DeleteCommand = new RelayCommand(Delete, () => SelectedRecord != null);
            UpdateCommand = new RelayCommand(Update, () => SelectedRecord != null);
            ResetCommand = new RelayCommand(Reset);
        }

        // PRIVATE METHODS FOR COMMANDS
        private void Add()
        {
            var addCourseScheduleWindow = new PT2.Views.CourseSchedules.AddCourseScheduleWindow();
            var addCourseScheduleViewModel = (AddCourseScheduleViewModel)addCourseScheduleWindow.DataContext;
            addCourseScheduleWindow.ShowDialog();

            if (addCourseScheduleViewModel.AddedCourseSchedule != null)
            {
                CourseSchedules.Add(addCourseScheduleViewModel.AddedCourseSchedule);
            }
        }

        private void Update()
        {
            var editViewModel = new EditCourseScheduleViewModel(SelectedRecord);
            var editWindow = new PT2.Views.CourseSchedules.EditCourseScheduleWindow();
            editWindow.DataContext = editViewModel;
            editViewModel.CloseAction = new Action(editWindow.Close);
            editWindow.ShowDialog();

            if (editViewModel.UpdatedCourseSchedule != null)
            {
                var oldSchedule = CourseSchedules.FirstOrDefault(cs => cs.TeachingScheduleId == editViewModel.UpdatedCourseSchedule.TeachingScheduleId);
                if (oldSchedule != null)
                {
                    CourseSchedules.Remove(oldSchedule);
                    CourseSchedules.Add(editViewModel.UpdatedCourseSchedule);
                }
            }
        }

        private void Delete()
        {
            var relatedRollCallBooks = _rollCallBookRepository.GetByScheduleId(SelectedRecord.TeachingScheduleId);

            if (relatedRollCallBooks.Any())
            {
                var result = MessageBox.Show($"This schedule has {relatedRollCallBooks.Count} roll call records. Deleting it will also delete all of its history. Are you sure you want to continue?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _rollCallBookRepository.removeRange(relatedRollCallBooks);

                }
                else
                {
                    return; // User cancelled
                }
            }
            _courseScheduleRepository.remove(SelectedRecord);
            CourseSchedules.Remove(SelectedRecord);
            SelectedRecord = null;
        }

        private void Reset()
        {
            SelectedCourse = null;
            SelectedRecord = null;
        }

        private void Filter()
        {
            FilteredCourseSchedules.Filter = item =>
            {
                if (SelectedCourse == null)
                    return true;

                var courseSchedule = item as CourseSchedule;
                return courseSchedule.CourseId == SelectedCourse.CourseId;
            };
        }

    }
}
