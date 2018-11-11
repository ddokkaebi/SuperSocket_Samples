using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    // 전체 연결된 세션의 상태 관리
    public class ConnectSessionManager
    {
        int MaxSessionCount = 0;
        List<ConnectSession> SessionList = new List<ConnectSession>();
        ConnectSession DisAbleSession = new ConnectSession();

        public void CreateSession(int maxCount)
        {
            MaxSessionCount = maxCount;

            for (int i = 0; i < maxCount; ++i)
            {
                SessionList.Add(new ConnectSession());
            }


            DisAbleSession.IsEnable = false;
        }

        public void SetLogin(int index)
        {
            var session = GetSession(index);
            session.SetStateLogin();
        }

        public bool SetPreRoomEnter(int index, int roomNumber)
        {
            var session = GetSession(index);
            return session.SetPreRoomEnter(roomNumber);
        }

        public bool SetRoomEntered(int index, int roomNumber)
        {
            var session = GetSession(index);
            return session.SetRoomEntered(roomNumber);
        }


        ConnectSession GetSession(int index)
        {
            if(0 <= index && index < MaxSessionCount)
            {
                return SessionList[index];
            }

            return DisAbleSession;
        }
        
    }
}
