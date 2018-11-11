using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    enum SessionStatus
    {
        NONE = 0,
        LOGIN = 1,
        ROOM_ENTERING = 2,
        ROOM = 3,
    }

    class ConnectSession
    {
        public bool IsEnable = true;
        int CurrentState = (int)SessionStatus.NONE;
        int RoomNumber = -1;

        public void Clear()
        {
            CurrentState = (int)SessionStatus.NONE;
            RoomNumber = -1;
        }

        public void SetStateLogin()
        {
            if (IsEnable == false)
            {
                return;
            }

            Interlocked.Exchange(ref CurrentState, (int)SessionStatus.LOGIN);
        }

        public bool SetPreRoomEnter(int roomNumber)
        {
            if (IsEnable == false)
            {
                return false;
            }

            var oldValue = Interlocked.CompareExchange(ref CurrentState, (int)SessionStatus.ROOM_ENTERING, (int)SessionStatus.LOGIN);
            if (oldValue != (int)SessionStatus.LOGIN)
            {
                return false;
            }

            Interlocked.Exchange(ref RoomNumber, roomNumber);
            return true;
        }

        public bool SetRoomEntered(int roomNumber)
        {
            if (IsEnable == false)
            {
                return false;
            }

            var oldValue = Interlocked.CompareExchange(ref CurrentState, (int)SessionStatus.ROOM, (int)SessionStatus.ROOM_ENTERING);
            if (oldValue != (int)SessionStatus.ROOM_ENTERING)
            {
                return false;
            }

            return true;
        }
    }
}
