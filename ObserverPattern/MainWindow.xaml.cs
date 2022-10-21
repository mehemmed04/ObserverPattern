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

namespace ObserverPattern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Repo UsersRepo { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            UsersRepo = new Repo();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            User user = UsersRepo.GetUserByUsername(UsernameTxtb.Text);
            if (user != null)
            {
                if (user.Password == PasswordTxtb.Text)
                {
                    SignInWindow window = new SignInWindow(user, UsersRepo);
                    window.ShowDialog();
                }
                else
                {
                    System.Windows.MessageBox.Show("Wrong Password");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("There is not such username");
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            User u = new User
            {
                Username = UsernameTxtb.Text,
                Password = PasswordTxtb.Text
            };
            UsersRepo.AddUser(u);
            System.Windows.MessageBox.Show("Registered succesfully");
            UsernameTxtb.Text = String.Empty;
            PasswordTxtb.Text = String.Empty;
        }
    }
}
