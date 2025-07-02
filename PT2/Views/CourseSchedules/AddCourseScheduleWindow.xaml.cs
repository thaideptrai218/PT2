using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PT2.Views.CourseSchedules
{
    /// <summary>
    /// Interaction logic for AddCourseScheduleWindow.xaml
    /// </summary>
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
