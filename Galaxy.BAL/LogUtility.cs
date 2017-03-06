using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.BAL
{
    public class LogUtility
    {
        public static void Fatal(string message, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info(string message)
        {
            Console.WriteLine(message);
            message += Environment.MachineName;
        }

        public static void EmailLog(string message)
        {

        }
    }
}
