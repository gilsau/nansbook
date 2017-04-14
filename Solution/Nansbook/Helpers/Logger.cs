using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using WebMatrix.WebData;

namespace Nansbook.Helpers
{
    public static class LogSvc
    {
        public static void LogError(string msg)
        {
            if (HttpContext.Current != null)
            {

                string dir = string.Format("{0}Logs", HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"].Replace("\\", "\\\\"));
                string dateStamp = string.Format("{0:yyyy.MM.dd}", DateTime.Now);
                string timeStamp = string.Format("{0:yyyy.MM.dd HH:mm:ss}", DateTime.Now);
                string fullPath = string.Format("{0}\\{1}.txt", dir, dateStamp);

                StreamWriter sw;

                //Create today's log file
                if (!File.Exists(fullPath))
                {
                    sw = File.CreateText(fullPath);
                }

                //Open today's log file
                else
                {
                    sw = File.AppendText(fullPath);
                }

                //Write error and time to log file
                sw.WriteLine();
                sw.WriteLine("*************************************************************************************************");
                sw.WriteLine(string.Format("{0} ||| {1} ||| {2}", timeStamp, WebSecurity.CurrentUserName, msg));

                sw.Close();
                sw.Dispose();
            }
        }
    }
}