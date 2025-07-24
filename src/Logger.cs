using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnityGERunner
{
    class Logger
    {
        public static string logPath = Path.GetFullPath("../../../engine.log");
        private static StreamWriter logWriter;
        public static bool disabled = false;

        public static void Init()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            if (!disabled)
            {
                logWriter = File.CreateText(logPath);
                logWriter.AutoFlush = true;
            }
            Console.WriteLine($"Logger started, output file: {logPath}");
        }

        private static void Log(string message)
        {
            if (logWriter != null) logWriter.WriteLine(message);
            Console.WriteLine(message);
        }

        public static void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Log("[INFO] " + message);
        }

        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Log("[WARN] " + message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Log("[ERROR] " + message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
