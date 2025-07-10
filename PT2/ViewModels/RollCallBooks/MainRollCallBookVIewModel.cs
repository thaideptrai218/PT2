using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PT2.Data;
using PT2.Data.Interfaces;
using PT2.Data.Repositories;
using PT2.Models;
using PT2.ViewModels.Base;
using System.Windows.Input;
using System.Windows;
using System.Runtime.InteropServices;

namespace PT2.ViewModels.RollCallBooks
{
    public class MainRollCallBookVIewModel : ViewModelBase
    {
        public ObservableCollection<RollCallBook> RollCallBooks { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<CourseSchedule> CourseSchedules { get; set; }
        public ICollectionView FilterdRollCallBooks { get; set; }
        public ICollectionView FilteredCourseSchdules { get; set; }

        private Course _selectedCourse;

        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
                Change();
            }
        }

        private CourseSchedule _selectedCourseSchedule;
        public CourseSchedule SelectedCourseSchedule
        {
            get => _selectedCourseSchedule;
            set
            {
                _selectedCourseSchedule = value;
                OnPropertyChanged();
                Filter();
            }
        }

        private IRollCallBookRepository _rollCallBooksRepository;
        private ICourseScheduleRepository _courseScheduleRepository;
        private ICourseRepository _courseRepository;

        public ICommand SaveChangesCommand { get; }

        public MainRollCallBookVIewModel()
        {
            _rollCallBooksRepository = new RollCallBookRepository();
            _courseScheduleRepository = new CourseScheduleRepository();
            _courseRepository = new CourseRepository();
            LoadData();
            SaveChangesCommand = new RelayCommand(SaveChanges);
            FilterdRollCallBooks = CollectionViewSource.GetDefaultView(RollCallBooks);
            FilterdRollCallBooks.Filter = (e) => false;
            FilteredCourseSchdules.Filter = (e) => false;
        }

        private void LoadData()
        {
            Courses = new ObservableCollection<Course>(_courseRepository.GetAll());
            RollCallBooks = new ObservableCollection<RollCallBook>(_rollCallBooksRepository.GetAll());
            CourseSchedules = new ObservableCollection<CourseSchedule>(_courseScheduleRepository.GetAll());
            FilteredCourseSchdules = CollectionViewSource.GetDefaultView(CourseSchedules);
        }

        private void Change()
        {

            FilteredCourseSchdules.Filter = (element) =>
            {
                if (SelectedCourse == null) return false;
                CourseSchedule e = (CourseSchedule)element;
                if (e == null) return false;
                return SelectedCourse.CourseId == e.CourseId;
            };
        }

        private void Filter()
        {
            FilterdRollCallBooks.Filter = (element) =>
            {
                if (SelectedCourseSchedule == null) return false;
                RollCallBook e = (RollCallBook)element;
                if (e == null) return false;
                return SelectedCourseSchedule.TeachingScheduleId == e.TeachingScheduleId;
            };
        }

        private void SaveChanges()
        {
            var result = MessageBox.Show("Are you sure to save", "Sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (MessageBoxResult.Yes == result)
            {
                _rollCallBooksRepository.SaveChanges();
                MessageBox.Show("Saved successfully", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
