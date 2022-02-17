using System.Windows;
using System.Windows.Input;
using System.Linq;
using System;
using System.Web.Security;
using System.Timers;
using System.Windows.Threading;

namespace AuthorizationOSSBSS
{
    public partial class Authorization: Window
    {
        string code = Membership.GeneratePassword(1, 0);//8 2 //Генерирует случайный код
        public Authorization()
        {
            InitializeComponent();
            passwordText.IsEnabled = false;
            codeText.IsEnabled = false;
            updateCodeButton.IsEnabled = false;
            enterButton.IsEnabled = false;       
        }

        private void NumberTextPreviewTextInput (object sender, TextCompositionEventArgs e) //Запрещает ввод не числовых значений номера
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void NumberTextKeyDown(object sender, KeyEventArgs e) //Выполняется при нажатии клавишей на текстовое поле Number
        {
            AuthorizationEntities dataContext = new AuthorizationEntities();

            if (e.Key == Key.Enter) //Определяет нажатие на клавишу Enter
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

        private void PasswordTextKeyDown(object sender, KeyEventArgs e) //Выполняется при нажатии клавишей на текстовое поле Password
        {
            AuthorizationEntities dataContext = new AuthorizationEntities();
            Timer timer = new Timer();

            if (e.Key == Key.Enter) 
            {
                string text = passwordText.Password;
              
                string password = dataContext.Users.Where(p => p.Password == text).Select(p => p.Password).FirstOrDefault();
                if (password == text)
                {
                    if (MessageBox.Show(code, "Код", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                        StartTimer();
                  
                    codeText.IsEnabled = true;
                    enterButton.IsEnabled = true;
                }

                else
                {
                    MessageBox.Show("Неверный пароль");
                }  
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e) //Очищает поля, делает кнопки не активными
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
            code = Membership.GeneratePassword(1, 0);//8 2
            MessageBox.Show(code, "Код", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EnterButtonClick(object sender, RoutedEventArgs e)
        {
            CheckCode();
        }

        private void TimerTick(object sender, EventArgs e) //Происходит по истечении интервала времени
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;

            CheckCode();
        }

        private void StartTimer() 
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void CheckCode() 
        {
            
            if (codeText.Text != code)
            {
                MessageBox.Show("Нажмите кнопку повторной отправки кода");
                updateCodeButton.IsEnabled = true;
               
                code = null;
            }
            else
            {
                AuthorizationEntities dataContext = new AuthorizationEntities();

                int text = Convert.ToInt32(numberText.Text);
                string role = dataContext.Persons.Where(p => p.User.Number == text).Select(p => p.Role.Role1).FirstOrDefault();
                MessageBox.Show("\tДобро пожаловать \n" + role);
            }
        }

        private void CodeTextKeyDown(object sender, KeyEventArgs e) //Выполняется при нажатии клавишей на текстовое поле Code
        {
            if (e.Key == Key.Enter)
            {
                CheckCode();
            }
        }
    }
}