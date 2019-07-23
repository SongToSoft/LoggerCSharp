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

    public enum ELoggerMode
    {
        CONSOLE,
        FILE,
        FILE_WITH_CONSOLE
    }

    public class Logger
    {
        private StreamWriter m_streamWriter;
        private ELoggerMode m_loggerMode;
        public Logger(ELoggerMode loggerMode)
        {
            m_loggerMode = loggerMode;
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
            Execute(loggerMessage);
        }

        public void Log<T>(string message, ELoggerLevel level, T value) where T : IComparable<T>
        {
            string loggerMessage = "[" + DateTime.Now + "]" + " [" + level + "] " + message + " [VALUE] : " + value;

            Execute(loggerMessage);
        }

        public void Log<T>(string message, ELoggerLevel level, T[] array) where T : IComparable<T>
        {
            string loggerMessage = "[" + DateTime.Now + "]" + " [" + level + "] " + message + " [ARRAY] : (";
            for (int i = 0; i < array.Length; ++i)
            {
                loggerMessage += array[i] + (i != (array.Length - 1) ? ", " : " )" );
            }

            Execute(loggerMessage);
        }

        public void Log<T>(string message, ELoggerLevel level, List<T> list) where T : IComparable<T>
        {
            string loggerMessage = "[" + DateTime.Now + "]" + " [" + level + "] " + message + " [LIST] : (";
            for (int i = 0; i < list.Count; ++i)
            {
                loggerMessage += list[i] + (i != (list.Count - 1) ? ", " : " )");
            }

            Execute(loggerMessage);
        }

        public void Execute(string loggerMessage)
        {
            if ((m_loggerMode == ELoggerMode.FILE) || (m_loggerMode == ELoggerMode.FILE_WITH_CONSOLE))
            {
                m_streamWriter.WriteLine(loggerMessage);
                m_streamWriter.Flush();
            }
            if ((m_loggerMode == ELoggerMode.CONSOLE) || (m_loggerMode == ELoggerMode.FILE_WITH_CONSOLE))
            {
                System.Console.WriteLine(loggerMessage);
            }
        }
    }
}
