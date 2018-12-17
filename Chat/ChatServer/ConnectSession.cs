using CSBaseLib;
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
        LOGIN_ING = 1,
        LOGIN = 2,
        ROOM_ENTERING = 3,
        ROOM = 4,
    }

    class ConnectSession
    {
        public bool IsEnable = true;
        int CurrentState = (int)SessionStatus.NONE;
        string UserID;
        Int64 RoomNumber = PacketDef.INVALID_ROOM_NUMBER;

        public void Clear()
        {
            CurrentState = (int)SessionStatus.NONE;
            RoomNumber = PacketDef.INVALID_ROOM_NUMBER;
        }

        public bool IsStateNone()
        {
            return (IsEnable && CurrentState == (int)SessionStatus.NONE);
        }

        public bool IsStateLogin()
        {
            return (IsEnable && CurrentState == (int)SessionStatus.LOGIN);
        }

        public void SetStateNone()
        {
            if (IsEnable)
            {
                CurrentState = (int)SessionStatus.NONE;
            }
        }

        public void SetStateLogin()
        {
            if (IsEnable)
            {
                CurrentState = (int)SessionStatus.LOGIN;
                Interlocked.Exchange(ref RoomNumber, PacketDef.INVALID_ROOM_NUMBER);
            }
         }

        public void SetStatePreLogin()
        {
            if (IsEnable)
            {
                CurrentState = (int)SessionStatus.LOGIN_ING;
            }           
        }

        public void SetStateLogin(string userID)
        {
            if (IsEnable == false)
            {
                return;
            }

            CurrentState = (int)SessionStatus.LOGIN;
            UserID = userID;
        }

        public int GetRoomNumber()
        {
            return (int)Interlocked.Read(ref RoomNumber);
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

        public bool SetRoomEntered(Int64 roomNumber)
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

            Interlocked.Exchange(ref RoomNumber, roomNumber);
            return true;
        }
    }
}
