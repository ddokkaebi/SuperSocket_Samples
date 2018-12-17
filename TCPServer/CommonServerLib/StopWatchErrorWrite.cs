using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServerLib
{
    public class StopWatchErrorWrite
    {
        public static void 재시작(System.Diagnostics.Stopwatch stopWatchWork)
        {
            stopWatchWork.Restart();
        }

        public static void 중단_후_시간_오버이면_로그출력(System.Diagnostics.Stopwatch stopWatchWork, long limitMS, string checkWork)
        {
            stopWatchWork.Stop();                                     // StopWatchDBWork 완료

            if (stopWatchWork.ElapsedMilliseconds >= limitMS)
            {
                var msg = string.Format("{0}. ProcTime limit over. expected:{1}ms, actuial:{2}ms", checkWork, limitMS, stopWatchWork.ElapsedMilliseconds);
                DBProcessor.WriteFileLog(msg, LOG_LEVEL.ERROR);
            }
        }
    }
}
