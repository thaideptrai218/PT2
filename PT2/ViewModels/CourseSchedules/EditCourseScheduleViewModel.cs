using PT2.Data;
using PT2.Data.Interfaces;
using PT2.Data.Repositories;
using PT2.Models;
using PT2.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PT2.ViewModels.CourseSchedules
{
    class EditCourseScheduleViewModel : ViewModelBase
    {
        public CourseSchedule editingCourseSchedule { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
            }
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public Action CloseAction { get; set; }
        private ICourseRepository _courseRepository;
        private IRoomRepository _roomRepository;
        private ICourseScheduleRepository _courseScheduleRepository;

        public EditCourseScheduleViewModel(CourseSchedule TargetCourseSchedule)
        {
            _courseRepository = new CourseRepository();
            _roomRepository = new RoomRepository();
            _courseScheduleRepository = new CourseScheduleRepository();
            editingCourseSchedule = TargetCourseSchedule;
            LoadData();
            SelectedCourse = Courses.FirstOrDefault(c => c.CourseId == editingCourseSchedule.CourseId);
            SelectedRoom = Rooms.FirstOrDefault(r => r.RoomId == editingCourseSchedule.RoomId);
            SaveCommand = new RelayCommand(UpdateCourseSchedule);
        }

        private void UpdateCourseSchedule()
        {
            if (SelectedCourse != null)
            {
                editingCourseSchedule.CourseId = SelectedCourse.CourseId;
            }
            if (SelectedRoom != null)
            {
                editingCourseSchedule.RoomId = SelectedRoom.RoomId;
            }

            _courseScheduleRepository.update(editingCourseSchedule); // Use the update method
            CloseAction?.Invoke();
        }

        private void LoadData()
        {
            Courses = new ObservableCollection<Course>(_courseRepository.GetAll());
            Rooms = new ObservableCollection<Room>(_roomRepository.GetAll());
        }
    }
}
