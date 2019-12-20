using System;
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
    }
}
