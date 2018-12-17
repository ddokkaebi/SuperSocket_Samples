using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using FileLogger = ServerLib.FileLogger;
using LOG_LEVEL = CommonServerLib.LOG_LEVEL;

namespace SocketServerForm
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 에러 핸들러 등록
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            System.Threading.Thread.GetDomain().UnhandledException += new UnhandledExceptionEventHandler(Program_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void Program_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                ShowError(ex, "UnhandledException");
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ShowError(e.Exception, "ThreadException");
        }

        static void ShowError(Exception ex, string title)
        {
            FileLogger.Write(string.Format("ChatServerLib에서 잡지 못한 예외 발생 ㅠㅠ. {0}", ex.Message), LOG_LEVEL.ERROR);
            FileLogger.Write(string.Format("[source] {0}", ex.Source), LOG_LEVEL.ERROR);
            FileLogger.Write(string.Format("[stacktrace] {0}", ex.StackTrace), LOG_LEVEL.ERROR);


            MessageBox.Show(string.Format("ChatServerLib에서 잡지 못한 예외 발생 ㅠㅠ. {0}", ex.Message), title);

            var filename = string.Format("{0}_UnhandledException.txt", DateTime.Now.ToString("yyyyMMDDHHmm"));
            var stream = new System.IO.StreamWriter(filename, true);
            stream.WriteLine("[" + title + "]");
            stream.WriteLine("[message]\r\n" + ex.Message);
            stream.WriteLine("[source]\r\n" + ex.Source);
            stream.WriteLine("[stacktrace]\r\n" + ex.StackTrace);
            stream.WriteLine();
            stream.Close();
        }
    }
}
