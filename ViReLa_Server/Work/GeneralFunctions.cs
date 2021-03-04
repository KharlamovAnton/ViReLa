using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.IO;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Reflection;
using System.Configuration;
using System.Net;
using Sgml;
using System.IO.Compression;

namespace ViReLa
{
    class GeneralFunctions
    {
        /// <summary>
        /// Создание базы данных
        /// </summary>
        public static void CreateDataBase()
        {
            Program.cwl("Проверка существования базы данных()");
            using (var mdb = new DbContextViReLa())
            {
                if(mdb.User.ToArray().Count() == 0)
                {
                    mdb.FillStandart();

                    mdb.SaveChanges();
                    Program.cwl("Создана стандартная база данных()");
                }
            }
            Program.cwl("База данных обнаружена()");
        }

        /// <summary>
        /// Обработка процессов
        /// </summary>
        public static void WorkServer()
        {
            using (var mdb = new DbContextViReLa())
            {
                var listProcess = mdb.ListProcess.OrderBy(x => x.id).Where(x => x.result == null || x.result == "");
                foreach (var process in listProcess)
                {
                    Program.cwl("Запуск процесса "+ process.typeProcess.pathToAppForProcess.path + "()");
                    var result = string.Empty;
                    using (var compiler = new System.Diagnostics.Process())
                    {
                        compiler.StartInfo.FileName = process.typeProcess.pathToAppForProcess.path;
                        compiler.StartInfo.Arguments = "<data>"+process.data+ "</data>";
                        compiler.StartInfo.UseShellExecute = false;
                        compiler.StartInfo.RedirectStandardOutput = true;
                        compiler.Start();
                        result=compiler.StandardOutput.ReadToEnd();
                        compiler.WaitForExit();
                    }
                    process.result = result;
                }
                mdb.SaveChanges();
            }
        }

        /// <summary>
        /// maybeLog
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public static void ErrorLog(string message)
        {
            File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLog.txt"),
                               System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString() + ":" + Environment.NewLine + message + Environment.NewLine);
        }
    }
}
