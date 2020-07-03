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
using WorkTimer.Model;

namespace WorkTimer
{
    /// <summary>
    /// Interaction logic for WorkTime.xaml
    /// </summary>
    public partial class WorkTime : Window
    {
        private UserModel _userData;
        public WorkTime(UserModel userData)
        {
            _userData = userData;
            InitializeComponent();
            this.Loaded += WorkTime_Loaded;
        }

        private void WorkTime_Loaded(object sender, RoutedEventArgs e)
        {
            Core.DataCore dataCore = new Core.DataCore();
            var usersWorkedTime = dataCore.GetUserWorkTime(_userData);

            DataGrid_Times.ItemsSource = usersWorkedTime;
        }
    }
}
