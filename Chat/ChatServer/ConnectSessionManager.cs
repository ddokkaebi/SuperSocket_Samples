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

        public int GetLobbyIndex(int index)
        {
            var session = GetSession(index);
            return session.GetLobbyIndex();
        }

        public bool EnableReuqestLogin(int index)
        {
            var session = GetSession(index);
            return session.IsStateNone();
        }

        public void SetPreLogin(int index)
        {
            var session = GetSession(index);
            session.SetStatePreLogin();
        }

        public void SetLogin(int index, string userID)
        {
            var session = GetSession(index);
            session.SetStateLogin(userID);
        }

        public void SetStateNone(int index)
        {
            var session = GetSession(index);
            session.SetStateNone();
        }

        public bool SetPreRoomEnter(int index, int roomNumber)
        {
            var session = GetSession(index);
            return session.SetPreRoomEnter(roomNumber);
        }

        public bool SetRoomEntered(int index, int lobbyIndex, int roomNumber)
        {
            var session = GetSession(index);
            return session.SetRoomEntered(lobbyIndex, roomNumber);
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
