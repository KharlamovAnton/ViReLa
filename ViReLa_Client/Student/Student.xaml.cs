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
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;


namespace ViReLa_Client.Student
{
    /// <summary>
    /// Логика взаимодействия для Student.xaml
    /// </summary>
    public partial class Student : Window
    {
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public int currentUserId { get; set; }
        /// <summary>
        /// Текущая вкладка
        /// </summary>
        public string currentView { get; set; }

        public Student(int id)
        {
            InitializeComponent();
            btAdd.Visibility = Visibility.Hidden;
            currentUserId = id;
            using (var mdb = new ViReLa.DbContextViReLa())
            {
                foreach (var propertie in mdb.GetType().GetProperties().Where(x => x.PropertyType.Name == "DbSet`1"))
                {
                    var tmpTagName = propertie.GetCustomAttributes(typeof(ViReLa.TagNameAttribute), true).FirstOrDefault();
                    if (tmpTagName != null)
                    {
                        var tagname = tmpTagName.GetType().GetProperty("tagName").GetValue(propertie.GetCustomAttributes(typeof(ViReLa.TagNameAttribute), true).FirstOrDefault());
                        twTree.Items.Add(tagname);
                    }
                    
                }
            }
        }

        private void windowsStudent_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        public void twTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            twTreeTree.Items.Clear();
            TreeView tvItem = (TreeView)sender;
            using (var mdb = new ViReLa.DbContextViReLa())
            {
                var currentTable = mdb.GetType().GetProperties().Where(x => 
                x.PropertyType.Name == "DbSet`1" &&
                x.GetCustomAttributes(typeof(ViReLa.TagNameAttribute), true).FirstOrDefault() != null &&
                x.GetCustomAttributes(typeof(ViReLa.TagNameAttribute), true).FirstOrDefault().GetType().GetProperty("tagName").GetValue(x.GetCustomAttributes(typeof(ViReLa.TagNameAttribute), true).FirstOrDefault()).ToString() == tvItem.SelectedItem.ToString()
                ).FirstOrDefault();

                switch (tvItem.SelectedItem.ToString())
                {
                    case "Информация":
                        btAdd.Visibility = Visibility.Hidden;
                        //dgContex.ItemsSource = mdb.User.Where(x => x.id == currentUserId).ToList();
                        currentView = "Информация";
                        twTreeTree.Items.Add("Личная");
                        break;
                    case "Группа":
                        btAdd.Visibility = Visibility.Hidden;
                        //dgContex.ItemsSource = mdb.Group.Where(x=>x.id== mdb.User.Where(y => y.id == currentUserId).FirstOrDefault().group_id ).ToList();
                        currentView = "Группа";
                        twTreeTree.Items.Add("Группа");
                        break;
                    case "Лабораторные работы":
                        btAdd.Visibility = Visibility.Visible;
                        //dgContex.ItemsSource = mdb.;
                        currentView = "Лабораторные работы";
                        foreach (var lp in mdb.ListProcess.Where(x => x.initiator_id == currentUserId).ToList())
                        {
                            var tmpTagName = lp.id;
                            twTreeTree.Items.Add(tmpTagName);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new ViReLa_Client.Student.WPFree(currentUserId,this);
            Hide();
            newWindow.Show();
        }

        private void twTreeTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView twTreeTreeItem = (TreeView)sender;
            
            using (var mdb = new ViReLa.DbContextViReLa())
            {
                var currentUser = mdb.User.Where(x => x.id == currentUserId).FirstOrDefault();
                switch (twTree.SelectedItem.ToString())
                {
                    case "Информация":
                        wbVisible.NavigateToString(WebUtility.HtmlDecode("<html><head><META http-equiv=Content-Type content=\"text/html; charset = utf-8\"></head><body><p><b>ID</b> " + currentUser.id + "</p></br><p><b>Имя</b> "+ currentUser.nameF + "</p></br><p><b>Роль</b> "+ currentUser.role.name+ "</p></br></body></html>"));
                        break;
                    case "Группа":
                        wbVisible.NavigateToString(WebUtility.HtmlDecode("<html><head><META http-equiv=Content-Type content=\"text/html; charset = utf-8\"></head><body><p><b>Группа</b> " + currentUser.group.name + "</p></br><p><b>Специальность</b> " + currentUser.group.specialty.name + "</p></br><p><b>Полное название</b> " + currentUser.group.specialty.info + "</p></br></body></html>"));
                        break;
                    case "Лабораторные работы":
                        var currentProcess = mdb.ListProcess.Where(x => x.initiator_id == currentUserId).FirstOrDefault();
                        if (twTreeTreeItem.SelectedItem != null)
                        {
                            currentProcess = mdb.ListProcess.Where(x => x.id.ToString() == twTreeTreeItem.SelectedItem.ToString()).FirstOrDefault();
                        }
                        var htmlView = String.Empty;
                        htmlView += "<html><head><META http-equiv=Content-Type content=\"text/html; charset = utf-8\"></head>";
                        htmlView += "<body>";
                        htmlView += "<p><b>Лабораторная работа: </b> " + currentProcess.typeProcess.name + "</p></br>";
                        htmlView += "<p><b>Предмет: </b> " + currentProcess.typeProcess.subjectArea.name + "</p></br>";
                        switch (currentProcess.typeProcess.viewProcess_id)
                        {
                            case 1:
                                var allElementResult = XElement.Parse(currentProcess.result).XPathSelectElements("*").ToArray();
                                var allElementTrueAnswer = XElement.Parse(currentProcess.typeProcess.pathToAppForProcess.scheme.resultScheme).XPathSelectElements("*").ToArray();
                                for (int i = 0; i < allElementResult.Length; i++)
                                {
                                    if(allElementResult[i].Value != allElementTrueAnswer[i].Value)
                                    {
                                        htmlView += "<p><font color=\"red\">" + allElementResult[i].Value + "</font></p></br>";
                                    }
                                    else
                                    {
                                        htmlView += "<p>" + allElementResult[i].Value + "</p></br>";
                                    }
                                    
                                }
                                break;
                            case 2:
                                allElementResult = XElement.Parse(currentProcess.data).XPathSelectElements("./Label").ToArray();
                                foreach (var element in allElementResult)
                                {
                                    htmlView += "<p>" + element.Value + "</p></br>";
                                }
                                allElementResult = XElement.Parse(currentProcess.data).XPathSelectElements("./TextBox").ToArray();
                                htmlView += "<table border=\"3\">";
                                var allData = new String[allElementResult[0].Value.Split(';').Length];
                                for (int i = 0; i < allElementResult.Length; i++)
                                {
                                    var dataXml = allElementResult[i].Value.Split(';');
                                    for (int j = 0; j < dataXml.Length; j++)
                                    {
                                        allData[j] = allData[j]+"<td>"+ dataXml[j]+ "</td>";
                                    }
                                    
                                }
                                foreach (var data in allData)
                                {
                                    htmlView += "<tr>" + data + "</tr>";
                                }
                                htmlView += "</table>";
                                htmlView += "<p><b>Результат: </b>" + currentProcess.result + "</p></br>";
                                break;

                        }
                        htmlView += "</body>";
                        htmlView += "</html>";


                        wbVisible.NavigateToString(WebUtility.HtmlDecode(htmlView));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
