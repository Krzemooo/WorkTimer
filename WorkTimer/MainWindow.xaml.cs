using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WorkTimer.Model;

namespace WorkTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserModel _userModel = new UserModel();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;
        NotifyIcon notifyIcon = new NotifyIcon();
        public MainWindow(UserModel userModel)
        {
            InitializeComponent();
            _userModel = userModel;
            this.Loaded += MainWindow_Loaded;
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                time_TextBox.Text = currentTime;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
            Start();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void btnBreak_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            Core.DataCore dataCore = new Core.DataCore();
            if (dataCore.AppendNewLine(DateTime.Now, (int)Model.TimeCheckpoinStatus.Break, _userModel))
            {
                stopWatch.Reset();
                time_TextBox.Text = "00:00:00";

                stopWatch.Start();
                dispatcherTimer.Start();

                btnBreak.IsEnabled = false;
                btnStart.IsEnabled = true;

                workStatus_Label.Content = "Przerwa od: ";
            }
        }

        private void Start()
        {
            Core.DataCore dataCore = new Core.DataCore();
            if (dataCore.AppendNewLine(DateTime.Now, (int)Model.TimeCheckpoinStatus.Start, _userModel))
            {
                stopWatch.Reset();
                time_TextBox.Text = "00:00:00";

                stopWatch.Start();
                dispatcherTimer.Start();


                btnBreak.IsEnabled = true;
                btnStart.IsEnabled = false;

                workStatus_Label.Content = "Praca od: ";
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;

            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.BalloonTipText = "Program został zminimalizowany do zasobnika.";
            notifyIcon.ShowBalloonTip(100);
            this.ShowInTaskbar = false;
            this.WindowState = WindowState.Minimized;
            notifyIcon.Visible = true;

            TrayMenuContext();
        }

        private void TrayMenuContext()
        {
            this.notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.notifyIcon.ContextMenuStrip.Items.Add("Pokaż", null, this.Context_Show_Click);
            this.notifyIcon.ContextMenuStrip.Items.Add("Ustawienia", null, this.Context_Setting_Click);
            this.notifyIcon.ContextMenuStrip.Items.Add("Exit", null, this.Context_Exit_Click);
        }

        private void Context_Exit_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Context_Setting_Click(object sender, EventArgs e)
        {
            ShowSetting();
        }

        private void Context_Show_Click(object sender, EventArgs e)
        {
            Maximize();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Maximize();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            ShowSetting();
        }

        private void ShowSetting()
        {
            Settings settings = new Settings(_userModel);
            settings.Show();
        }

        private void Maximize()
        {
            if (WindowState == System.Windows.WindowState.Minimized)
                this.WindowState = WindowState.Normal;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Core.DataCore dataCore = new Core.DataCore();
            dataCore.AppendNewLine(DateTime.Now, (int)Model.TimeCheckpoinStatus.Break, _userModel);
        }
    }
}
