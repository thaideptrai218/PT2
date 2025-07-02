using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PT2.Data;
using PT2.Data.Interfaces;
using PT2.Data.Repositories;
using PT2.Models;
using PT2.ViewModels.Base;

namespace PT2.ViewModels.CourseSchedules;

public class AddCourseScheduleViewModel : ViewModelBase
{
    public ObservableCollection<Course> Courses { get; set; }
    public ObservableCollection<Room> Rooms { get; set; }
    public CourseSchedule NewCourseSchedule { get; set; }
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

    public ICommand AddCommand { get; set; }
    public Action CloseAction { get; set; }
    public CourseSchedule AddedCourseSchedule { get; private set; }
    private ICourseRepository _courseRepository;
    private IRoomRepository _roomRepository;
    private ICourseScheduleRepository _courseScheduleRepository;

    public AddCourseScheduleViewModel()
    {
        _courseRepository = new CourseRepository();
        _roomRepository = new RoomRepository();
        _courseScheduleRepository = new CourseScheduleRepository();
        NewCourseSchedule = new CourseSchedule();
        LoadData();
        AddCommand = new RelayCommand(AddCourseSchedule);
    }

    private void LoadData()
    {
        Courses = new ObservableCollection<Course>(_courseRepository.GetAll());
        Rooms = new ObservableCollection<Room>(_roomRepository.GetAll());
    }

    private void AddCourseSchedule()
    {
        if (SelectedCourse != null)
        {
            NewCourseSchedule.CourseId = SelectedCourse.CourseId;
        }
        if (SelectedRoom != null)
        {
            NewCourseSchedule.RoomId = SelectedRoom.RoomId;
        }

        if (NewCourseSchedule.CourseId == 0 || NewCourseSchedule.RoomId == 0)
        {
            System.Windows.MessageBox.Show("Please select a course and a room.");
            return;
        }

        _courseScheduleRepository.add(NewCourseSchedule);
        this.AddedCourseSchedule = NewCourseSchedule;

        CloseAction?.Invoke();
    }
}
