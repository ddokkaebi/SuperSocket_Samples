using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using ServerLogger = ServerLib.FileLogger;

namespace SocketServerForm
{
    public partial class MainForm : Form
    {
        System.Windows.Threading.DispatcherTimer workProcessTimer = new System.Windows.Threading.DispatcherTimer();

        void CreateFormWorkThread()
        {
            workProcessTimer.Tick += new EventHandler(OnProcessTimedEvent);
            workProcessTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);
            workProcessTimer.Start();
        }

        private void OnProcessTimedEvent(object sender, EventArgs e)
        {
            try
            {
                ProcessLog();
                ProcessInnerMessage();
                //ProcessIPCMessage();
            }
            catch (Exception ex)
            {
                ServerLogger.Write(string.Format("[OnProcessTimedEvent] Exception:{0}", ex.ToString()), CommonServerLib.LOG_LEVEL.ERROR);
            }
        }

        private void ProcessLog()
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int logWorkCount = 0;

            while (true)
            {
                string msg;

                if (ServerLib.DevLog.GetLog(out msg))
                {
                    ++logWorkCount;

                    if (listBoxLog.Items.Count > 512)
                    {
                        listBoxLog.Items.Clear();
                    }

                    listBoxLog.Items.Add(msg);
                    listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
                }
                else
                {
                    break;
                }

                if (logWorkCount > 32)
                {
                    break;
                }
            }
        }

        void ProcessInnerMessage()
        {
            // 너무 이 작업만 할 수 없으므로 일정 작업 이상을 하면 일단 패스한다.
            int workedCount = 0;

            while (true)
            {
                CommonServerLib.InnerMsg msg;

                if (CommonServerLib.MsgToHostProgram.GetMsg(out msg))
                {
                    ++workedCount;

                    switch (msg.Type)
                    {
                        case CommonServerLib.InnerMsgType.SERVER_START:
                            var values = msg.Value1.Split("_");
                            textBoxServerID.Text = values[0];
                            textBoxAddress.Text = values[1];
                            break;

                        case CommonServerLib.InnerMsgType.CREATE_COMPONENT:
                            textBoxLobbyStartIndex.Text = ServerLib.ServerEnvironment.LobbyStartIndex.ToString();
                            textBoxLobbyCount.Text = ServerLib.ServerEnvironment.LobbyCount.ToString();
                            textBoxMaxUserPerLobby.Text = ServerLib.ServerEnvironment.MaxUserPerLobby.ToString();
                            break;

                        case CommonServerLib.InnerMsgType.CURRENT_CONNECT_COUNT:
                            textBoxCurrentUserCount.Text = msg.Value1;
                            break;

                        case CommonServerLib.InnerMsgType.CURRENT_LOBBY_INFO:
                            {
                                var tokens = msg.Value1.Split("_");

                                int iItemNum = listViewLobbyInfo.Items.Count;

                                for (int i = 0; i < iItemNum; ++i)
                                {
                                    if (tokens[0] == listViewLobbyInfo.Items[i].SubItems[0].Text)
                                    {
                                        listViewLobbyInfo.Items[i].SubItems[1].Text = tokens[1];
                                        listViewLobbyInfo.Refresh();
                                        return;
                                    }
                                }
                            }
                            break;

                        case CommonServerLib.InnerMsgType.UPDATE_UI_INFO:
                            {
                                var tokens = msg.Value1.Split("_");

                                textBoxCurrentUserCount.Text = tokens[0];

                                int iItemNum = listViewLobbyInfo.Items.Count;
                                for (int i = 0; i < iItemNum; ++i)
                                {
                                    listViewLobbyInfo.Items[i].SubItems[1].Text = tokens[i + 1];
                                }

                                listViewLobbyInfo.Refresh();
                            }
                            break;
                    }
                }
                else
                {
                    break;
                }

                if (workedCount > 32)
                {
                    break;
                }
            }
        }

        //void ProcessIPCMessage()
        //{
        //    try
        //    {
        //        var packet = IPCCommu.GetPacketData();

        //        if (packet == null)
        //        {
        //            return;
        //        }

        //        switch ((IPC_MSG_TYPE)packet.Item1)
        //        {
        //            case IPC_MSG_TYPE.REQUEST_HEALTH_CHECK:
        //                {
        //                    DevLog.Write(string.Format("[REQUEST_HEALTH_CHECK] {0}", packet.Item2), LOG_LEVEL.INFO);

        //                    var response = new IPCMsgReSponseHealthCheck() { CurUserCount = textBoxCurrentUserCount.Text };
        //                    var jsonstring = Util.ToJsonFormatString<IPCMsgReSponseHealthCheck>(response);
        //                    IPCCommu.SendMessage((short)IPC_MSG_TYPE.RESPONSE_HEALTH_CHECK, jsonstring);
        //                }
        //                break;

        //            default:
        //                {
        //                    if ((short)packet.Item1 == ProcessCommunicate.ProcessCommu.IPC_NETWORK_ERROR)
        //                    {
        //                        var message = string.Format("[ProcessIPCMessage] Error:{0}", packet.Item2);
        //                        DevLog.Write(message, LOG_LEVEL.ERROR);
        //                        ServerLogic.WriteFileLog(message, LOG_LEVEL.ERROR);
        //                    }
        //                }
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DevLog.Write(string.Format("[ProcessIPCMessage] Exception:{0}", ex.ToString()), LOG_LEVEL.ERROR);
        //        ServerLogic.WriteFileLog(string.Format("[ProcessIPCMessage] Exception:{0}", ex.ToString()), LOG_LEVEL.ERROR);
        //    }
        //}
        //void ProcessIPCMessage2()
        //{
        //    try
        //    {
        //        var packet = IPCCommu.ServerReceive();
        //        if (packet.PacketIndex == 0)
        //        {
        //            return;
        //        }

        //        if (packet.PacketIndex == ProcessCommunicate.ProcessCommu.PACKET_INDEX_DISCONNECT)
        //        {
        //            DevLog.Write("IPC Receive 불가", LOG_LEVEL.ERROR);
        //        }

        //        switch ((IPC_MSG_TYPE)packet.PacketIndex)
        //        {
        //            case IPC_MSG_TYPE.REQUEST_HEALTH_CHECK:
        //                {
        //                    DevLog.Write(string.Format("[REQUEST_HEALTH_CHECK]"), LOG_LEVEL.INFO);

        //                    var response = new IPCMsgReSponseHealthCheck() { CurUserCount = textBoxCurrentUserCount.Text };
        //                    var jsonstring = Util.ToJsonFormatString<IPCMsgReSponseHealthCheck>(response);
        //                    IPCCommu.SendMessage((short)IPC_MSG_TYPE.RESPONSE_HEALTH_CHECK, jsonstring);
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DevLog.Write(string.Format("[ProcessIPCMessage] Exception:{0}", ex.ToString()), LOG_LEVEL.ERROR);
        //        ServerLogic.WriteFileLog(string.Format("[ProcessIPCMessage] Exception:{0}", ex.ToString()), LOG_LEVEL.ERROR);
        //    }
        //}
    }
}
