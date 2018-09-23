using MessagePack; //https://github.com/neuecc/MessagePack-CSharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBaseLib
{
    public class PacketToBytes
    {
        public static byte[] Make(PACKETID packetID, Int16 lobbyID, byte[] bodyData)
        {
            List<byte> dataSource = new List<byte>();
            dataSource.AddRange(BitConverter.GetBytes((Int32)packetID));
            dataSource.AddRange(BitConverter.GetBytes(lobbyID));
            dataSource.AddRange(BitConverter.GetBytes((Int16)0));

            if (bodyData != null)
            {
                dataSource.AddRange(BitConverter.GetBytes(bodyData.Length));
                dataSource.AddRange(bodyData);
            }
            else
            {
                dataSource.AddRange(BitConverter.GetBytes((Int32)0));
            }

            return dataSource.ToArray();
        }

        public static Tuple<int, byte[]> ClientReceiveData(int recvLength, byte[] recvData)
        {
            var packetID = BitConverter.ToInt32(recvData, 0);

            var packetBody = new byte[recvLength];
            Buffer.BlockCopy(recvData, 12, packetBody, 0, (recvLength - 12));

            return new Tuple<int, byte[]>(packetID, packetBody);
        }
    }

    // 로그인 요청
    [MessagePackObject]
    public class PKTReqLogin
    {
        [Key(0)]
        public string UserID;
        [Key(1)]
        public string AuthToken;
    }

    public class PKTResLogin
    {
        public short Result;
    }


    [MessagePackObject]
    public class PKTReqRoomEnter
    {
        [Key(0)]
        public int RoomId;
    }

    public class PKTResRoomEnter
    {
        public short Result;
    }

    [MessagePackObject]
    public class PKTNtfRoomUserList
    {
        [Key(0)]
        public List<string> UserNickNames;
    }

    [MessagePackObject]
    public class PKTNtfRoomNewUser
    {
        [Key(0)]
        public string UserNickName;
    }


    public class PKTReqRoomLeave
    {
    }

    public class PKTResRoomLeave
    {
        public short Result;
    }

    [MessagePackObject]
    public class PKTNtfRoomLeaveUser
    {
        [Key(0)]
        public string UserNickName;
    }


    [MessagePackObject]
    public class PKTReqRoomChat
    {
        [Key(0)]
        public string ChatMessage;
    }

    public class PKTResRoomChat
    {
        public short Result;
    }

    [MessagePackObject]
    public class PKTNtfRoomChat
    {
        [Key(0)]
        public string UserNickName;

        [Key(1)]
        public string ChatMessage;
    }
}
