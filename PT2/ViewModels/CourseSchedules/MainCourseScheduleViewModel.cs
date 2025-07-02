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

        private string _filteredText;
        public string FilteredText
        {
            get => _filteredText;
            set
            {
                _filteredText = value;
                OnPropertyChanged();
                Filter();
            }
        }

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
            DeleteCommand = new RelayCommand(Delete, () => SelectedCourseSchedule != null);
            UpdateCommand = new RelayCommand(Update, () => SelectedCourseSchedule != null);
            ResetCommand = new RelayCommand(Reset);
        }

        // PRIVATE METHODS FOR COMMANDS
        private void Add()
        {

        }

        private void Update()
        {

        }

        private void Delete()
        {

        }

        private void Reset()
        {
        }

        private void Filter()
        {
            FilteredCourseSchedules.Filter = item =>
            {
                if (string.IsNullOrEmpty(FilteredText))
                    return true;

                var courseSchedule = item as CourseSchedule;
                return courseSchedule.CourseId.ToString().Contains(FilteredText, StringComparison.OrdinalIgnoreCase);
            };
        }

    }
}
