using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBaseLib
{
    public class PacketToBytes
    {
        public static int HeaderSize = 12;

        public static byte[] Make(PACKETID packetID, byte[] bodyData)
        {
            List<byte> dataSource = new List<byte>();
            dataSource.AddRange(BitConverter.GetBytes((Int32)packetID));
            dataSource.AddRange(BitConverter.GetBytes((Int16)0));
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
                
        public static Tuple<int, byte[]> ClientReceiveData(int recvLength, ArraySegment<byte> recvData)
        {
            if (recvLength <= 0)
            {
                return new Tuple<int, byte[]>((int)PACKETID.SYSTEM_DISCONNECT_CLIENT, null);
            }

            var packetID = BitConverter.ToInt32(recvData.Array, recvData.Offset);

            var packetBody = new byte[recvLength];
            Buffer.BlockCopy(recvData.Array, (recvData.Offset+12), packetBody, 0, (recvLength - 12));

            return new Tuple<int, byte[]>(packetID, packetBody);
        }
    }

    
    // 서버에서 접속 종료를 통보
    public class NTFDisConnect
    {
        public short ErrorCode;
    }

    // 허트 비트 요청
    public class PKTReqHeartBeat
    {
        public int DummyValue;
    }

    public class PKTResHeartBeat
    {
        public int DummyValue;
    }


    // 로그인 요청
    public class PKTReqLogin
    {
        public string UserID;       
        public string AuthToken;
    }

    public class PKTResLogin
    {
        public short Result;
    }


    // 로비 들어가기 요청
    public class PKTReqEnterLobby
    {
        public int LobbyID;
    }

    public class PKTResEnterLobby
    {
        public short Result;
    }


    // 로비 나가기 요청
    public class PKTReqLeaveLobby
    {
        public int LobbyID;
    }

    public class PKTResLeaveLobby
    {
        public short Result;
    }


    // 로비 채팅
    public class PKTReqLobbyChat
    {
        public string ChatMsg;
    }

    public class PKTNtfLobbyChat
    {
        public short Result;
        public string NickName;
        public string ChatMsg;
    }


    // 서버의 알림 메시지
    public class PKTNotification
    {
        public string Msg;
    }


    // 자신의 종료 요청
    public class PKTReqOwnDisconnect
    {
        public string UserID;
        public string AuthToken;
    }

    public class PKTResOwnDisconnect
    {
        public short Result;
    }


    // 로비 채팅
    public struct PKTReqGuildChat
    {
        public string Msg;
    }


    // 길드 채팅
    public struct PKTNtfGuildChat
    {
        public short Result;
        public string Name;     // 닉네임
        public string Msg;      // 채팅 메시지
    }


    // 유저의 길드 정보 로딩
    public class PKTReqLoadUserGuildInfo
    {
        public Int64 GuildUnique;
    }

    public class PKTResLoadUserGuildInfo
    {
        public short Result;
    }





    /// 채팅 서버간 메시지 포맷
    public struct S2SMsgDisConnect
    {
        public string UserID;
        public int LobbyID;
    }

    public struct S2SMsgGuildChat
    {
        public Int64 GU;    // GuildUnique
        public string Name; // NickName
        public string Msg;
    }
}
