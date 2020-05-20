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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WorkTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            var temp = Core.FileFolderCore.GetDayWorkTimes(DateTime.Now);
            //;
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            Core.DataCore dataCore = new Core.DataCore();
            dataCore.AppendNewLine(DateTime.Now, (int)Model.TimeCheckpoinStatus.Break);
        }
    }
}
