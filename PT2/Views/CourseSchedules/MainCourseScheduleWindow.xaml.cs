using PT2.ViewModels.CourseSchedules;
using System.Windows;

namespace PT2.Views.CourseSchedules
{
    public partial class MainCourseScheduleWindow : Window
    {
        public MainCourseScheduleWindow()
        {
            InitializeComponent();
            DataContext = new MainCourseScheduleViewModel();
        }
    }
}
