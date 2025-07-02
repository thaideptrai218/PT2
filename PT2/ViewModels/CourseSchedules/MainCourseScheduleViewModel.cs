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

        }

        private void Delete()
        {

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
