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

    // 로그아웃 요청
    public class PKTResLogout
    {
        public short Result;
    }
}
