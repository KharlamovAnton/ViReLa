using System;
using System.Threading;

namespace ViReLa
{
    class Program
    {

        public static void cwl(string s)
        {
            var consoleLine = String.Empty;
            for (int j = 0; j < s.Length + 2; j++)
            {
                consoleLine = consoleLine + "█";
            }
            Console.WriteLine(consoleLine);
            Console.WriteLine("█" + s+ "█");
            Console.WriteLine(consoleLine);
        }


        static void Main(string[] args)
        {
            try
            {
                cwl("Начало работы сервера()");
                GeneralFunctions.CreateDataBase();
                while (true)
                {
                    cwl("Работа в штатном режиме()");
                    GeneralFunctions.WorkServer();
                    cwl("Процессы завершены()"+DateTime.Now.ToString());
                    Thread.Sleep(5000);
                }
            }
            catch (Exception e)
            {
                GeneralFunctions.ErrorLog(e.ToString());
            }
        }
    }
}
