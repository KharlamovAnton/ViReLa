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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using Sgml;
using System.Web;
using System.IO;
using System.Net;

namespace ViReLa_Client.Student
{

    public class myColumns
    {
        public string name { get; set; }
        public string value { get; set;}

    }

    /// <summary>
    /// Логика взаимодействия для WPFree.xaml
    /// </summary>
    public partial class WPFree : Window
    {
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public int currentUserId { get; set; }

        /// <summary>
        /// Предыдущее окно
        /// </summary>
        public Student studentObj { get; set; }

        public string currentTypeName { get; set; }

        public string xmlData { get; set; }

        public WPFree(int id, Student student)
        {
            InitializeComponent();
            currentUserId = id;
            studentObj = student;

            using (var mdb = new ViReLa.DbContextViReLa())
            {
                foreach (var typeProcess in mdb.TypeProcess.ToList())
                {
                    var myBtn = new Button() { Content = typeProcess.name };
                    wpTypeProcess.Children.Add(myBtn);
                    myBtn.Margin = new Thickness(10);
                    myBtn.Click += new RoutedEventHandler(CreateButton_Click);
                }
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var thisBtn = (Button)sender;
            currentTypeName = thisBtn.Content.ToString();
            gDataResult.Children.Clear();
            wpTypeProcess.Children.Clear();
            var myBtn = new Button() { Content = "Сохранить" };
            myBtn.Margin = new Thickness(5);
            myBtn.Click += new RoutedEventHandler(SaveButton_Click);
            wpTypeProcess.Children.Add(myBtn);

            using (var mdb = new ViReLa.DbContextViReLa())
            {
                var typeProcess = mdb.TypeProcess.Where(x => x.name == thisBtn.Content.ToString()).FirstOrDefault();

                xmlData = typeProcess.pathToAppForProcess.scheme.dataScheme.ToString();

                var top = 30;
                var _Label = new Label() { Content = "Лабораторная работа:" + typeProcess.name + ". По предмету:" + typeProcess.subjectArea.name, FontSize = top };
                _Label.Margin = new Thickness(5, 0, 0, 0);
                gDataResult.Children.Add(_Label);

                var allElement = XElement.Parse(xmlData).XPathSelectElements("*").ToArray();
                for (int i = 0; i < allElement.Length; i++)
                {
                    switch (allElement[i].Name.ToString())
                    {
                        case "Label":
                            top = top + 20;
                            _Label = new Label() { Content = allElement[i].Value };
                            _Label.Margin = new Thickness(5, top, 0, 0);
                            gDataResult.Children.Add(_Label);
                            break;
                        case "TextBox":
                            top = top + 20;
                            var _TextBox = new TextBox();
                            _TextBox.HorizontalAlignment = HorizontalAlignment.Left;
                            _TextBox.VerticalAlignment = VerticalAlignment.Top;
                            _TextBox.Height = 20;
                            _TextBox.Width = 100;
                            _TextBox.Margin = new Thickness(5, top, 0, 0);
                            gDataResult.Children.Add(_TextBox);
                            break;
                        case "ComboBox":
                            top = top + 20;
                            var _ComboBox = new ComboBox();
                            _ComboBox.Width = 80;
                            _ComboBox.Height = 20;
                            var results = allElement[i].XPathSelectElements("./ComboBoxItem").ToArray();
                            foreach (var item in results)
                            {
                                _ComboBox.Items.Add(item.Value);
                            }
                            _ComboBox.HorizontalAlignment = HorizontalAlignment.Left;
                            _ComboBox.VerticalAlignment = VerticalAlignment.Top;
                            _ComboBox.Margin = new Thickness(10, top, 0, 0);
                            _ComboBox.SelectedIndex = 0;
                            gDataResult.Children.Add(_ComboBox);
                            break;
                        default:
                            break;
                    }
                }
                
            }
            
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var result = String.Empty;
            var allElement = XElement.Parse(xmlData).XPathSelectElements("*").ToArray();
            for (int i = 0; i < allElement.Length; i++)
            {
                switch (allElement[i].Name.ToString())
                {
                    case "TextBox":
                        var _TextBox = (TextBox)gDataResult.Children[i + 1];
                        result = _TextBox.Text ;
                        allElement[i].SetValue(result);
                        break;
                    case "ComboBox":
                        var _ComboBox = (ComboBox)gDataResult.Children[i + 1];
                        result = "<ComboBoxItem>" + _ComboBox.SelectedItem + "</ComboBoxItem>";
                        allElement[i].SetValue(result);
                        break;
                    default:
                       
                        break;
                }
            }
            result = "<standart>";
            using (var mdb = new ViReLa.DbContextViReLa())
            {
                
                var newLP = new ViReLa.ListProcess();
                newLP.typeProcess_id = mdb.TypeProcess.Where(x => x.name == currentTypeName).FirstOrDefault().id;
                newLP.initiator_id = currentUserId;
                foreach (var element in allElement)
                {
                    result += WebUtility.HtmlDecode(element.ToString());

                }
                switch (newLP.typeProcess_id)
                {
                    case 1:
                        newLP.data = mdb.TypeProcess.Where(x => x.name == currentTypeName).FirstOrDefault().pathToAppForProcess.scheme.dataScheme.ToString();
                        newLP.result = result + "</standart>";
                        break;
                    case 2:
                        newLP.data = result + "</standart>";
                        newLP.result = "";
                        break;
                }
                
                
                mdb.ListProcess.Add(newLP);
                mdb.SaveChanges();
            }
            studentObj.twTree_SelectedItemChanged(studentObj.twTree, null);
            studentObj.Show();
            Hide();
        }


        private void windowWPFree_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
