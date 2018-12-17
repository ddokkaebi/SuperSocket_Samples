using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.CompilerServices;
using System.Threading;

using LOG_LEVEL = CommonServerLib.LOG_LEVEL;
using LOG_STRING_TYPE = CommonServerLib.LOG_STRING_TYPE;

namespace ServerLib
{
    public class FileLogger
    {
        static log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Write(string msg, LOG_LEVEL logLevel = LOG_LEVEL.TRACE,
                                [CallerFilePath] string fileName = "",
                                [CallerMemberName] string methodName = "",
                                [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                var sourceFileName = System.IO.Path.GetFileName(fileName);
                //var logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                var logMsg = string.Format("{0} {1} {2}[Line] | {3}", sourceFileName, methodName, lineNumber, msg);

                switch (logLevel)
                {
                    case LOG_LEVEL.INFO:
                        Task.Run(() => Logger.Info(logMsg));
                        break;
                    case LOG_LEVEL.ERROR:
                        Task.Run(() => Logger.Error(logMsg));
                        break;
                    case LOG_LEVEL.DEBUG:
                        Task.Run(() => Logger.Debug(logMsg));
                        break;
                    default:
                        Task.Run(() => Logger.Error(string.Format("{0}:{1} {2} {3}| 지원하지 않은 로그 레벨 사용", DateTime.Now, fileName, methodName, lineNumber)));
                        break;
                }

                DevLog.Write(logMsg, logLevel);
            }
            catch
            {
            }
        }
    }

    public class DevLog
    {
        static System.Collections.Concurrent.ConcurrentQueue<string> logMsgQueue = new System.Collections.Concurrent.ConcurrentQueue<string>();

        static Int64 출력가능_로그레벨 = (Int64)LOG_LEVEL.TRACE;

        static bool[] EnableLogStringTypeList = new bool[(int)LOG_STRING_TYPE.END];


        static public void Init(LOG_LEVEL logLevel)
        {
            ChangeLogLevel(logLevel);
        }

        static public void ChangeLogLevel(LOG_LEVEL logLevel)
        {
            Interlocked.Exchange(ref 출력가능_로그레벨, (int)logLevel);
        }

        public static LOG_LEVEL CurrentLogLevel()
        {
            var curLogLevel = (LOG_LEVEL)Interlocked.Read(ref 출력가능_로그레벨);
            return curLogLevel;
        }
                
        static public void Write(string msg, LOG_LEVEL logLevel = LOG_LEVEL.TRACE,
                                [CallerFilePath] string fileName = "",
                                [CallerMemberName] string methodName = "",
                                [CallerLineNumber] int lineNumber = 0)
        {
            if (CurrentLogLevel() <= logLevel)
            {
                logMsgQueue.Enqueue(string.Format("{0}:{1}| {2}", DateTime.Now, methodName, msg));
            }
        }

        static public bool GetLog(out string msg)
        {
            if (logMsgQueue.TryDequeue(out msg))
            {
                return true;
            }

            return false;
        }

        public static void EnableDisable(LOG_STRING_TYPE type, bool enable)
        {
            EnableLogStringTypeList[(int)type] = enable;
        }

        public static bool IsEnable(LOG_STRING_TYPE type)
        {
            return EnableLogStringTypeList[(int)type];
        }
    }

    }
