using Microsoft.EntityFrameworkCore;
using PractWarehouse2.ModelsDB;
using System.Windows;

namespace PractWarehouse2
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            using(Pract4Warehouse1Context _db = new Pract4Warehouse1Context())
            {
                var user = _db.Users
                   .FirstOrDefault(u => u.UserLogin == tbLogin.Text && u.UserPassword == tbPassword.Password);

              
                if (user != null)
                {
                    Data.UserLastname = user.UserLastname ?? "";
                    Data.UserName = user.UserName ?? "";
                    Data.UserPatronymic = user.UserPatronymic ?? "";
                    _db.UserRoles.Load();
                    Data.UserRole = user.UserRole?.Role ?? "Пользователь";
                    Data.Enter = true;
                    
                    Close();

                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации",
                        MessageBoxButton.OK, MessageBoxImage.Warning);

                    
                    tbLogin.Focus();
                }
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnEnterHowGuest(object sender, RoutedEventArgs e)
        {
            Data.UserLastname = "Гость";
            Data.UserName = "";
            Data.UserPatronymic = "";
            Data.UserRole = "Пользователь";
            Data.Enter = true;
            Close();
        }
    }
}
