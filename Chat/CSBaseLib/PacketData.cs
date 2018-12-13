using MessagePack; //https://github.com/neuecc/MessagePack-CSharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBaseLib
{
    public class PacketDef
    {
        public const Int16 PACKET_HEADER_SIZE = 5;
        public const int MAX_USER_ID_BYTE_LENGTH = 16;
        public const int MAX_USER_PW_BYTE_LENGTH = 16;
    }

    public class PacketToBytes
    {
        public static byte[] Make(PACKETID packetID, byte[] bodyData)
        {
            Int16 bodyDataSize = 0;
            if (bodyData != null)
            {
                bodyDataSize = (Int16)bodyData.Length;
            }
            var packetSize = bodyDataSize + PacketDef.PACKET_HEADER_SIZE;

            List<byte> dataSource = new List<byte>();
            dataSource.AddRange(BitConverter.GetBytes((Int16)packetSize));
            dataSource.AddRange(BitConverter.GetBytes((Int16)packetID));
            dataSource.AddRange(BitConverter.GetBytes((Int16)0));

            if (bodyData != null)
            {
                dataSource.AddRange(bodyData);
            }
            
            return dataSource.ToArray();
        }

        public static Tuple<int, byte[]> ClientReceiveData(int recvLength, byte[] recvData)
        {
            var packetSize = BitConverter.ToInt16(recvData, 0);
            var packetID = BitConverter.ToInt16(recvData, 2);
            var bodySize = packetSize - PacketDef.PACKET_HEADER_SIZE;

            var packetBody = new byte[bodySize];
            Buffer.BlockCopy(recvData, PacketDef.PACKET_HEADER_SIZE, packetBody,  0, bodySize);

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

    [MessagePackObject]
    public class PKTResLogin
    {
        [Key(0)]
        public short Result;
    }


    [MessagePackObject]
    public class PKTReqRoomEnter
    {
        [Key(0)]
        public int RoomNumber;
    }

    [MessagePackObject]
    public class PKTResRoomEnter
    {
        [Key(0)]
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


    [MessagePackObject]
    public class PKTReqRoomLeave
    {
    }

    [MessagePackObject]
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

    
    [MessagePackObject]
    public class PKTNtfRoomChat
    {
        [Key(0)]
        public string UserID;

        [Key(1)]
        public string ChatMessage;
    }
}
