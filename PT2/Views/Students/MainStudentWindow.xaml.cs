using PT2.ViewModels.Students;
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

namespace PT2.Views.Students
{
    /// <summary>
    /// Interaction logic for MainStudentWindow.xaml
    /// </summary>
    public partial class MainStudentWindow : Window
    {
        public MainStudentWindow()
        {
            InitializeComponent();
            DataContext = new MainStudentWindowViewModel();
        }
    }
}
