using System.Windows;
using System.Windows.Input;
using System.Linq;
using System;
using System.Web.Security;
using System.Timers;

namespace AuthorizationOSSBSS
{
    public partial class Authorization: Window
    {
        public Authorization()
        {
            InitializeComponent();
            passwordText.IsEnabled = false;
            codeText.IsEnabled = false;
            updateCodeButton.IsEnabled = false;
            enterButton.IsEnabled = false;       
        }

        private void NumberTextPreviewTextInput (object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void NumberTextKeyDown(object sender, KeyEventArgs e)
        {
            AuthorizationEntities dataContext = new AuthorizationEntities();

            if (e.Key == Key.Enter)
            {
                int text = Convert.ToInt32(numberText.Text);
                int number = dataContext.Users.Where(p => p.Number == text).Select(p => p.Number).FirstOrDefault();
                if (number == text)
                {
                    passwordText.IsEnabled = true;
                    passwordText.Focus();
                }

                else
                {
                    MessageBox.Show("Номера нет в базе данных");
                }
            }
        }

        private void PasswordTextKeyDown(object sender, KeyEventArgs e)
        {
            AuthorizationEntities dataContext = new AuthorizationEntities();
            Timer timer = new Timer();

            if (e.Key == Key.Enter)
            {
                string text = passwordText.Password;
                string code = Membership.GeneratePassword(3, 0);//8 2
                string password = dataContext.Users.Where(p => p.Password == text).Select(p => p.Password).FirstOrDefault();
                if (password == text)
                {
                    if (MessageBox.Show(code, "Код", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        // timer start
                    
                    enterButton.IsEnabled = true;



                    if (code != codeText.Text) // || timer закончился  //после ввода сделать, сейчас раньше срабатывает
                    {
                        MessageBox.Show("Нажмите кнопку повторной отправки кода");
                    }
                }

                else
                {
                    MessageBox.Show("Неверный пароль");
                }
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            numberText.Text = "";
            passwordText.Password = "";
            codeText.Text = "";

            passwordText.IsEnabled = false;
            codeText.IsEnabled = false;
            updateCodeButton.IsEnabled = false;
            enterButton.IsEnabled = false;
        }

        private void UpdateCodeButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void EnterButtonClick(object sender, RoutedEventArgs e)
        {
            AuthorizationEntities dataContext = new AuthorizationEntities();

            int text = Convert.ToInt32(numberText.Text);
            string role = dataContext.Persons.Where(p => p.User.Number == text).Select(p => p.Role.Role1).FirstOrDefault();
            MessageBox.Show("Добро пожаловать " + role);
        }
    }
}


