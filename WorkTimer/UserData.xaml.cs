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
    /// Interaction logic for UserData.xaml
    /// </summary>
    public partial class UserData : Window
    {
        public UserData()
        {
            InitializeComponent();

#if DEBUG
            textBoxName.Text = "Dominik";
            textBoxSurname.Text = "Krzemiński";
#endif
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text.Trim()) || string.IsNullOrEmpty(textBoxName.Text.Trim()))
            {
                MessageBox.Show("Pole imię oraz nazwisko muszą zostać wypełnione!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                UserModel userModel = new UserModel() { Name = textBoxName.Text, Surname = textBoxSurname.Text };
                MainWindow mainWindow = new MainWindow(userModel);
                this.Close();
                mainWindow.Show();
            }
        }
    }
}
