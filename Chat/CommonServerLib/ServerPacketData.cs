using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;

namespace CommonServerLib
{
    public class ServerPacketData
    {
        public Int16 PacketSize;
        public string SessionID;
        public Int16 PacketID;        
        public SByte Type;
        public byte[] BodyData;

        
        public void Assign(string sessionID, EFBinaryRequestInfo reqInfo)
        {
            SessionID = sessionID;

            PacketSize = reqInfo.Size;
            PacketID = reqInfo.PacketID;
            Type = reqInfo.Type;
            
            if (reqInfo.Body.Length > 0)
            {
                BodyData = reqInfo.Body;
            }
        }

        public void Assign(DBResultQueue DBResult)
        {
            SessionID = DBResult.SessionID;

            PacketID = (short)DBResult.PacketID;
            BodyData = DBResult.Datas;
        }

        public static ServerPacketData MakeNTFInConnectOrDisConnectClientPacket(bool isConnect, string sessionID)
        {
            var packet = new ServerPacketData();
            
            if (isConnect)
            {
                packet.PacketID = (Int32)PACKETID.NTF_IN_CONNECT_CLIENT;
            }
            else
            {
                packet.PacketID = (Int32)PACKETID.NTF_IN_DISCONNECT_CLIENT;
            }

            packet.SessionID = sessionID;
            return packet;
        }

        //public static ServerPacketData MakeNTFWrongUserPacket(WRONG_USER_TYPE type, string sessionID)
        //{
        //    var packet = new ServerPacketData();
        //    packet.PacketID = (Int32)PACKETID.SYSTEM_WRONG_USER;
        //    packet.SessionID = sessionID;
        //    packet.Value1 = 0;
        //    packet.Value2 = 0;
        //    packet.JsonFormatData = ((short)type).ToString();
        //    return packet;
        //}
    }

    
}
