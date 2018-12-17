using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;

namespace CommonServerLib
{
    public enum REDIS_TYPE
    {
        GAME = 1,
        CHAT = 2,
    }


    struct ChatToChatMessage
    {
        public string Type;
        public Int64 ST;       // SecondTime
        public string Msg;     // Message
    }

    public class DBQueue
    {
        public PACKETID PacketID;
        public string SessionID;
        public string JsonFormatData;
    }

    public struct DBResultQueue
    {
        public PACKETID PacketID;
        public string SessionID;
        public short LobbyID;
        public string JsonFormatData;
    }


    public struct DBReqRedisWriteString
    {
        public REDIS_TYPE UseRedisType;
        public string Key;
        public string Value;
    }

    public struct DBReqRedisReadValue
    {
        public string Key;
    }

    public struct S2SMessageData
    {
        public string From;
        public string Type;
        public string Message;
    }
    public struct DBResReadS2SMessage
    {
        public List<S2SMessageData> MessageList;
    }


    public struct DBReqLogin
    {
        public string UserID;
        public string AuthToken;
    }

    public struct DBResLogin
    {
        public ERROR_CODE Result;
        public string UserID;
        public string NickName;
        public Int64 GuildUnique;
    }


    public struct DBReqEnterLobby
    {
        public string UserID;
        public int LobbyID;
    }

    public struct DBResEnterLobby
    {
        public ERROR_CODE Result;
        public string UserID;
        public int LobbyID;
    }


    public struct DBReqLeaveLobby
    {
        public string UserID;
        public int LobbyID;
    }

    public struct DBResLeaveLobby
    {
        public ERROR_CODE Result;
        public string UserID;
        public int LobbyID;
    }


    public struct DBReqOwnDisconnect
    {
        public string UserID;
        public string AuthToken;
    }


    public struct DBNtfGuildChat
    {
        public Int64 GU;    // GuildUnique
        public string Name; // NickName
        public string Msg;
    }


    public struct DBReqUserGuildInfo
    {
        public string UserID;
    }

    public struct DBResUserGuildInfo
    {
        public ERROR_CODE Result;
        public string UserID;
        public Int64 GuildUnique;
    }
}
