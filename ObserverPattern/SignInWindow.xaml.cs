using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ObserverPattern
{
    public partial class SignInWindow : Window
    {
        public User user { get; set; }
        public Repo repo { get; set; } 
        public SignInWindow(User u,Repo P)
        {
            InitializeComponent();
            user = u;
            repo = P;
            this.DataContext = this;
            UsersListView.ItemsSource = null;
            UsersListView.ItemsSource = repo.Users;
            NotificationsListView.ItemsSource = null;
            NotificationsListView.ItemsSource = user.Notifications;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = true };
            bool? response = openFileDialog.ShowDialog();
            if (response == true)
            {
                string[] files = openFileDialog.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    string filename = System.IO.Path.GetFullPath(files[i]);
                    FileInfo fileInfo = new FileInfo(files[i]);
                    var converter = new ImageSourceConverter();
                    ImageBrush img = new ImageBrush();
                    img.ImageSource =
                        (ImageSource)converter.ConvertFromString(filename);
                    btn.Background = img;
                }
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            repo.SetUsersFromFile();   
            if (toogleButton.IsChecked == true)
            {
                SubscribersListView.ItemsSource = null;
                SubscribersListView.ItemsSource = user.Subscribers;
            }
            else
            {
                SubscribersListView.ItemsSource = null;
            }
        }

        private void SubscribeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var u = UsersListView.SelectedItem as User;
                u.Subscribers.Add(user);
                var n = new Notification { Message = $"{user.Username} Subscribed" };
                u.Notifications.Add(n);
                repo.Update();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Select User");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var item in user.Subscribers)
            {
                var u = repo.Users.FirstOrDefault(x => x.Username == item.Username);
                u.Notifications.Add(new Notification
                {
                    Message = $"{user.Username} Added Post"
                });
            }
            repo.Update();
        }
    }
}
