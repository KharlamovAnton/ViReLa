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

namespace ViReLa_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var mdb = new ViReLa.DbContextViReLa()) 
            {
                var currentUser = mdb.User.Where(x => (x.nameS.Trim() == tbForLogin.Text) && (pbForPass.Password == x.password.password.Trim())).FirstOrDefault();
                if (currentUser != null)
                {
                    MessageBox.Show("Здравствуйте,"+ currentUser.nameF + ". Ваша авторизация успешно выполнена. Нажмите Ок и ожидайте загрузку лаборатории");
                    
                    var newWindow = new Window();
                    if (currentUser.role.name.Trim() == "admin") newWindow = new ViReLa_Client.Admin.Admin(currentUser.id);
                    if (currentUser.role.name.Trim() == "student") newWindow = new ViReLa_Client.Student.Student(currentUser.id);
                    Close();
                    newWindow.Show();
                }
                else
                {
                    MessageBox.Show("Не удаётся войти. Пожалуйста, проверьте правильность написания логина и пароля.");
                }
            }
        }
    }
}
