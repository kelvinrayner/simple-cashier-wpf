using CRUDBootcamp32.Context;
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

namespace CRUDBootcamp32
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MyContext myContext = new MyContext();

        public Login()
        {
            InitializeComponent();
            myContext.Users.ToList();
            myContext.Roles.ToList();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsernameLogin.Text;
            string password = PbxPasswordLogin.Password;

            if(!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                var user = myContext.Users.FirstOrDefault(u => u.Email.Equals(username) && u.Password.Equals(password));
                if (user != null)
                {
                    MessageBox.Show("Login succesfull!");
                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login Unsuccesfull!");
                }
            }
            else if (String.IsNullOrEmpty(username))
            {
                MessageBox.Show("Fill username");
            }
            else if (String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Fill password");
            }
        }

        private void BtnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
            this.Close();
        }
    }
}
