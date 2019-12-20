using System;
using System.Collections.Generic;
using System.IO;

namespace UnhandledException
{
    class ErrorHandler
    {
        public static void Catch(string Func_Name, Exception exception, string additional_info = "")
        {
            string LocalStorage = Cons.MyDocuments + "\\_Maoci_Logs\\";
            if (!Directory.Exists(LocalStorage))
                Directory.CreateDirectory(LocalStorage);
            File.WriteAllText(
                LocalStorage + Func_Name + ".log", 
                "ErrorStart >>>>>>>>>>>>>" + Environment.NewLine + 
                additional_info + Environment.NewLine + 
                exception.Message + "|" + Environment.NewLine + 
                exception.Source + "|" + Environment.NewLine + 
                exception.StackTrace + Environment.NewLine + 
                "ErrorEnds <<<<<<<<<<<<<" + Environment.NewLine
            );
        }


        public static void Log(string s_Func_Name, string s_Log_Msg)
        {
            string LocalStorage = Cons.MyDocuments + "\\_Maoci_Logs\\";
            if (!Directory.Exists(LocalStorage))
                Directory.CreateDirectory(LocalStorage);

            IEnumerable<string> IE_S_Log = new List<string>() { s_Func_Name, s_Log_Msg };

            File.AppendAllLines(LocalStorage, IE_S_Log);
        }
    }
}
