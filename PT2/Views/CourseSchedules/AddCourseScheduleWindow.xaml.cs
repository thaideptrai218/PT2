using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PT2.Views.CourseSchedules
{
    public partial class AddCourseScheduleWindow : Window
    {
        public AddCourseScheduleWindow()
        {
            InitializeComponent();
            var viewModel = new PT2.ViewModels.CourseSchedules.AddCourseScheduleViewModel();
            DataContext = viewModel;
            viewModel.CloseAction = new Action(this.Close);
        }
    }
}
