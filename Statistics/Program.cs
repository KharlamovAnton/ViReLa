using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;

namespace Statistics
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var xmlAnswer = XElement.Parse(String.Join(" ", args)).XPathSelectElements("./standart/TextBox").ToArray();
                var answerBreak = xmlAnswer[0].Value.Split(';');
                var answerWeight = xmlAnswer[1].Value.Split(';');
                float result = 0;
                for (int i = 0; i < answerBreak.Length; i++)
                {
                    result = result + (float.Parse(answerBreak[i]) * float.Parse(answerWeight[i]));
                }
                Console.WriteLine(result);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
