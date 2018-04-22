using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace PasswordManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
      
        private MasterPasswordContext _context = new MasterPasswordContext();
        public MainWindow()
        {
            InitializeComponent();
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(120) };
            timer.Tick += delegate
            {
                timer.Stop();
                Logout();
                timer.Start();
            };
            timer.Start();
            InputManager.Current.PostProcessInput += delegate (object s, ProcessInputEventArgs r)
            {
                if (r.StagingItem.Input is MouseButtonEventArgs || r.StagingItem.Input is KeyEventArgs)
                {
                    timer.Interval = TimeSpan.FromSeconds(120);
                }
            };

            InitializeComponent();
      
        }


        public void Restart()
        {
            HeaderOptions.IsEnabled = true;
            LoginLayer.Visibility = Visibility.Collapsed;
        }
        public void Logout()
        {
            LoginLayer.Visibility = Visibility.Visible;
            HeaderOptions.IsEnabled = false;
            txtPassword.Password = string.Empty;
        }
     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void MenuItem_Click2a(object sender, RoutedEventArgs e)
        {
            Stage.Child = new SetMasterPwd();
        }

        private void MenuItem_Click2b(object sender, RoutedEventArgs e)
        {
            Stage.Child = new SetDomain();
        }

        private void MenuItem_Click2c(object sender, RoutedEventArgs e)
        {
            Stage.Child = new ShowPasswords();
        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            LoginLayer.Visibility = Visibility.Visible;
            HeaderOptions.IsEnabled = false;
            txtPassword.Password = string.Empty;
            
        }
       
        private void Login_Click(object sender, RoutedEventArgs e)
        {
           
            bool isPass = Model.Authentication.CheckMasterPassword(txtPassword.Password);
            if (isPass)
            {
                HeaderOptions.IsEnabled = true;
                LoginLayer.Visibility = Visibility.Collapsed;
                PwdGrid.Background = Brushes.White;

            }
            else
            {
                HeaderOptions.IsEnabled = false;
                LoginLayer.Visibility = Visibility.Visible;
                PwdGrid.Background = Brushes.Red;
                
            }
           

        }
        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void PasswordDownHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                bool isPass = Model.Authentication.CheckMasterPassword(txtPassword.Password);
                if (isPass)
                {
                    HeaderOptions.IsEnabled = true;
                    LoginLayer.Visibility = Visibility.Collapsed;
                    PwdGrid.Background = Brushes.White;

                }
                else
                {
                    HeaderOptions.IsEnabled = false;
                    LoginLayer.Visibility = Visibility.Visible;
                    PwdGrid.Background = Brushes.Red;

                }
            }
        }
    }
}
