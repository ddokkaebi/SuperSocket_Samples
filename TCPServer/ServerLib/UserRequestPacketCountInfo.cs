using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{    
    // 유저가 보낸 패킷 횟수 정보
    class UserRequestPacketCountInfo
    {
        // 패킷 정보 저장 최대 횟수. 서버가 받은 시간을 저장.
        int MaxSendCountRow;
        
        // 현재 패킷을 받은 시간을 저장한 저장소의 인덱스
        int CurTimeIndex;

        // 패킷을 받은 시간을 저장한 저장소
        TimeLineCount[] RequestPacketTimeCount;


        public void Init(int maxSendCountRow, int maxRequestCount)
        {
            MaxSendCountRow = maxSendCountRow;
            RequestPacketTimeCount = new TimeLineCount[MaxSendCountRow];

            for (int i = 0; i < MaxSendCountRow; ++i)
            {
                RequestPacketTimeCount[i] = new TimeLineCount() { Time = 0, Count = 0 };
            }
        }

        public void Clear()
        {
            CurTimeIndex = 0;

            for (int i = 0; i < MaxSendCountRow; ++i)
            {
                RequestPacketTimeCount[i].Time = 0;
                RequestPacketTimeCount[i].Count = 0;
            }
        }

        public void Record(Int64 curTimeSec)
        {
            if (RequestPacketTimeCount[CurTimeIndex].Time == 0 ||
                RequestPacketTimeCount[CurTimeIndex].Time == curTimeSec)
            {
                RequestPacketTimeCount[CurTimeIndex].Time = curTimeSec;
                ++RequestPacketTimeCount[CurTimeIndex].Count;
                return;
            }

            ++CurTimeIndex;
            if (CurTimeIndex >= MaxSendCountRow)
            {
                CurTimeIndex = 0;
            }

            RequestPacketTimeCount[CurTimeIndex].Time = curTimeSec;
            RequestPacketTimeCount[CurTimeIndex].Count = 1;
        }

        public bool CheckHeavuRequest()
        {
            for (int i = 0; i < MaxSendCountRow; ++i)
            {
                if (RequestPacketTimeCount[i].Count >= CommonServerLib.ServerDefineData.MAX_CLIENT_REQUEST_PER_SECOND)
                {
                    return true;
                }
            }

            return false;
        }


        struct TimeLineCount
        {
            public Int64 Time;
            public int Count;
        }
    }
}
