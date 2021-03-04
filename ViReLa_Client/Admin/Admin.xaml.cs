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
using System.Data.Entity;

namespace ViReLa_Client.Admin
{

    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {

        /// <summary>
        /// Идентификатор текущего пользователя
        /// </summary>
        public int currentid { get; set; }

        /// <summary>
        /// Текущая вкладка
        /// </summary>
        public string currentView { get; set; }

        ViReLa.DbContextViReLa mdb;

        DataGrid dgContex;
        public Admin(int cID)
        {
            InitializeComponent();
            this.currentid = cID;
            mdb = new ViReLa.DbContextViReLa();
            foreach (var propertie in mdb.GetType().GetProperties().Where(x => x.PropertyType.Name == "DbSet`1"))
            {
                var tmpTagName = propertie.GetCustomAttributes(typeof(ViReLa.RussianNameAttribute), true).FirstOrDefault();
                if (tmpTagName != null)
                {
                    var tagname = tmpTagName.GetType().GetProperty("russianName").GetValue(propertie.GetCustomAttributes(typeof(ViReLa.RussianNameAttribute), true).FirstOrDefault());
                    twTree.Items.Add(tagname);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void twTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tvItem = (TreeView)sender;
            mdb = new ViReLa.DbContextViReLa();

            var currentTable = mdb.GetType().GetProperties().Where(x =>
            x.PropertyType.Name == "DbSet`1" &&
            x.GetCustomAttributes(typeof(ViReLa.RussianNameAttribute), true).FirstOrDefault().GetType().GetProperty("russianName").GetValue(x.GetCustomAttributes(typeof(ViReLa.RussianNameAttribute), true).FirstOrDefault()).ToString() == tvItem.SelectedItem.ToString()
            ).FirstOrDefault();
            dgContex = new DataGrid();
            dgContex.AutoGenerateColumns = true;
            switch (tvItem.SelectedItem.ToString())
            {
                case "Пользователи":
                    gFree.Children.Clear();
                    mdb.User.Load();
                    dgContex.ItemsSource = mdb.User.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Специальности":
                    gFree.Children.Clear();
                    mdb.Specialty.Load();
                    dgContex.ItemsSource = mdb.Specialty.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Группы":
                    gFree.Children.Clear();
                    mdb.Group.Load();
                    dgContex.ItemsSource = mdb.Group.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Роли":
                    gFree.Children.Clear();
                    mdb.Role.Load();
                    dgContex.ItemsSource = mdb.Role.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Виды процессов":
                    gFree.Children.Clear();
                    mdb.ViewProcess.Load();
                    dgContex.ItemsSource = mdb.ViewProcess.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Предметы":
                    gFree.Children.Clear();
                    mdb.SubjectArea.Load();
                    dgContex.ItemsSource = mdb.SubjectArea.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Приложения":
                    gFree.Children.Clear();
                    mdb.PathToAppForProcess.Load();
                    dgContex.ItemsSource = mdb.PathToAppForProcess.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Типы процессов":
                    gFree.Children.Clear();
                    mdb.TypeProcess.Load();
                    dgContex.ItemsSource = mdb.TypeProcess.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Список процессов":
                    gFree.Children.Clear();
                    mdb.ListProcess.Load();
                    dgContex.ItemsSource = mdb.ListProcess.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Пароли":
                    gFree.Children.Clear();
                    mdb.Password.Load();
                    dgContex.ItemsSource = mdb.Password.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
                case "Схемы":
                    gFree.Children.Clear();
                    mdb.SchemeDR.Load();
                    dgContex.ItemsSource = mdb.SchemeDR.Local.ToBindingList();
                    gFree.Children.Add(dgContex);
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mdb.SaveChanges();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dgContex.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgContex.SelectedItems.Count; i++)
                {
                    var dgContexSI = dgContex.SelectedItems[i] as ViReLa.User;
                    if (dgContexSI != null)
                    {
                        mdb.User.Remove(dgContexSI);
                    }
                }
            }
            mdb.SaveChanges();
        }
    }
}
