using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSBaseLib;


namespace ServerLib
{
    class UserGuildManager
    {
        Dictionary<Int64, LinkedList<ConnectUser>> UserGuildMap = new Dictionary<Int64, LinkedList<ConnectUser>>();

        public void AddGuild(ConnectUser user)
        {
            if (user.GuildUnique == 0)
            {
                return;
            }

            var guildUserList = GetGuildUserList(user.GuildUnique);
            if (guildUserList == null)
            {
                guildUserList = new LinkedList<ConnectUser>();
                UserGuildMap.Add(user.GuildUnique, guildUserList);
            }

            guildUserList.AddLast(user);
        }

        public void RemoveGuild(ConnectUser user)
        {
            var guildUserList = GetGuildUserList(user.GuildUnique);
            if (guildUserList == null)
            {
                return;
            }

            guildUserList.Remove(user);
        }

        public LinkedList<ConnectUser> GetGuildUserList(Int64 guildUnique)
        {
            LinkedList<ConnectUser> guildUserList = null;
            UserGuildMap.TryGetValue(guildUnique, out guildUserList);
            return guildUserList;
        }

        public void GuildChatting(ServerNetwork network, Int64 guildUnique, string nickName, string chatMsg)
        {
            var guildUserList = GetGuildUserList(guildUnique);
            if (guildUserList == null)
            {
                return;
            }

            var sendChatData = PKHLobby.MakeGuildChatPacket(ERROR_CODE.NONE, nickName, chatMsg);

            guildUserList.ForEach(user =>
            {
                if (user.EnableNetworkInLobby())
                {
                    network.SendData(user.SessionID, sendChatData, PACKETID.NTF_GUILD_CHAT);
                }
            }
                );
        }
    }
}
