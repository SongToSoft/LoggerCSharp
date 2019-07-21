using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LoggerSpace
{
    public enum ELoggerLevel
    {
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL
    }

    public class Logger
    {
        private StreamWriter m_streamWriter;
        private bool m_withConsoleOutput = false;
 
        public Logger(bool withConsoleOutput = true)
        {
            m_withConsoleOutput = withConsoleOutput;
            string fileName = (Assembly.GetCallingAssembly().GetName().Name + "_" + DateTime.Now + ".txt").Replace(' ', '_').Replace(':', '.');
            if (File.Exists(fileName))
            {
                m_streamWriter = new StreamWriter(fileName);
            }
            else
            {
                m_streamWriter = new StreamWriter(File.Create(fileName));
            }
            Log("Logger was started", ELoggerLevel.INFO);
        }

        ~Logger()
        {
            Log("Logger was finished", ELoggerLevel.INFO);
        }

        public void Log(string message, ELoggerLevel level, Exception exception = null)
        {
            string loggerMessage = "[" + DateTime.Now + "]" + " [" + level + "] " + message;
            if (exception != null)
            {
                loggerMessage += "[EXCEPTION] " + exception.Message;
            }
            m_streamWriter.WriteLine(loggerMessage);
            m_streamWriter.Flush();
            if (m_withConsoleOutput)
            {
                System.Console.WriteLine(loggerMessage);
            }
        }

        public void Log<T>(string message, ELoggerLevel level, T value) where T : IComparable<T>
        {
            string loggerMessage = "[" + DateTime.Now + "]" + " [" + level + "] " + message + " [VALUE] : " + value;

            m_streamWriter.WriteLine(loggerMessage);
            m_streamWriter.Flush();
            if (m_withConsoleOutput)
            {
                System.Console.WriteLine(loggerMessage);
            }
        }

        public void Log<T>(string message, ELoggerLevel level, T[] array) where T : IComparable<T>
        {
            string loggerMessage = "[" + DateTime.Now + "]" + " [" + level + "] " + message + " [ARRAY] : (";
            for (int i = 0; i < array.Length; ++i)
            {
                loggerMessage += array[i] + (i != (array.Length - 1) ? ", " : " )" );
            }

            m_streamWriter.WriteLine(loggerMessage);
            m_streamWriter.Flush();
            if (m_withConsoleOutput)
            {
                System.Console.WriteLine(loggerMessage);
            }
        }

        public void Log<T>(string message, ELoggerLevel level, List<T> list) where T : IComparable<T>
        {
            string loggerMessage = "[" + DateTime.Now + "]" + " [" + level + "] " + message + " [LIST] : (";
            for (int i = 0; i < list.Count; ++i)
            {
                loggerMessage += list[i] + (i != (list.Count - 1) ? ", " : " )");
            }

            m_streamWriter.WriteLine(loggerMessage);
            m_streamWriter.Flush();
            if (m_withConsoleOutput)
            {
                System.Console.WriteLine(loggerMessage);
            }
        }
    }
}
