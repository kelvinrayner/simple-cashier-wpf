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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        MyContext myContext = new MyContext();
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void BtnSubmitForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            string name = TxtNameUserForgotPassword.Text;
            string email = TxtEmailUserForgotPassword.Text;
            string newPassword = Guid.NewGuid().ToString();

            var match = myContext.Users.FirstOrDefault(u => u.Name == name && u.Email == email);

            if (!String.IsNullOrEmpty(TxtNameUserForgotPassword.Text) && !String.IsNullOrEmpty(TxtEmailUserForgotPassword.Text))
            {
                if (match != null)
                {
                    var update = myContext.Users.Where(o => o.Email == email).First();

                    if (update != null)
                    {
                        update.Password = newPassword;
                        var modify = myContext.SaveChanges();

                        if(modify > 0)
                        {
                            MessageBox.Show("Your new pasword has been changed to " + newPassword);
                            CreateEmailSentNewPassword(email, newPassword);
                            MessageBox.Show("Your new pasword has been sent to " + email);
                            Login login = new Login();
                            login.Show();
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Not Match!");
                }
            }
            else if (String.IsNullOrEmpty(TxtNameUserForgotPassword.Text))
            {
                MessageBox.Show("Name must be fill!");
            }
            else if(String.IsNullOrEmpty(TxtEmailUserForgotPassword.Text))
            {
                MessageBox.Show("Email must be fill!");
            }
        }

        private void CreateEmailSentNewPassword(String email, String password)
        {
            Microsoft.Office.Interop.Outlook.Application application = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.MailItem mailItem = application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
            mailItem.Subject = "Generate New Change Password sent to email exercise";
            mailItem.To = email;
            mailItem.Body = "Succesfully change your password! Your new password : " + password + ". Please login with username " + email + " and new password " + password;
            mailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
            mailItem.Display(false);
        }
    }
}
