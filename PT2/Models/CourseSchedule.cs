using PT2.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PT2.Models;

public partial class CourseSchedule : ModelBase
{
    private int _teachingScheduleId;
    [Key]
    public int TeachingScheduleId
    {
        get => _teachingScheduleId;
        set
        {
            _teachingScheduleId = value;
            OnPropertyChanged();
        }
    }

    private int? _courseId;
    public int? CourseId
    {
        get => _courseId;
        set
        {
            _courseId = value;
            OnPropertyChanged();
        }
    }

    private DateTime? _teachingDate;
    public DateTime? TeachingDate
    {
        get => _teachingDate;
        set
        {
            _teachingDate = value;
            OnPropertyChanged();
        }
    }

    private int? _slot;
    public int? Slot
    {
        get => _slot;
        set
        {
            _slot = value;
            OnPropertyChanged();
        }
    }

    private int? _roomId;
    public int? RoomId
    {
        get => _roomId;
        set
        {
            _roomId = value;
            OnPropertyChanged();
        }
    }

    private string? _description;
    public string? Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    private Course? _course;
    public virtual Course? Course
    {
        get => _course;
        set
        {
            _course = value;
            OnPropertyChanged();
        }
    }

    private ICollection<RollCallBook> _rollCallBooks = new List<RollCallBook>();
    public virtual ICollection<RollCallBook> RollCallBooks
    {
        get => _rollCallBooks;
        set
        {
            _rollCallBooks = value;
            OnPropertyChanged();
        }
    }

    private Room? _room;
    public virtual Room? Room
    {
        get => _room;
        set
        {
            _room = value;
            OnPropertyChanged();
        }
    }
}
