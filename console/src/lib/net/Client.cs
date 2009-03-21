﻿/***************************************************************************
 *   Copyright (C) 2008 by Lothar May                                      *
 *                                                                         *
 *   This file is part of pokerth_console.                                 *
 *   pokerth_console is free software: you can redistribute it and/or      *
 *   modify it under the terms of the GNU Affero General Public License    *
 *   as published by the Free Software Foundation, either version 3 of     *
 *   the License, or (at your option) any later version.                   *
 *                                                                         *
 *   pokerth_console is distributed in the hope that it will be useful,    *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the                                *
 *   GNU Affero General Public License along with pokerth_console.         *
 *   If not, see <http://www.gnu.org/licenses/>.                           *
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace pokerth_lib
{
	public class Client
	{
		public Client(Settings settings, PokerTHData data, ICallback callback)
		{
			m_tcpClient = new TcpClient();
			m_settings = settings;
			m_data = data;
			m_callback = callback;
		}

		public void Connect()
		{
			// Connect to the server.
			// If the user specified a server, try this.
			if (m_settings.ServerSettings.Server.Length > 0)
				m_tcpClient.Connect(
					m_settings.ServerSettings.Server, m_settings.ServerSettings.Port);
			else
			{
				// Try IPv6 first.
				/*IPAddress[] addresses = new IPAddress[2];
				addresses[0] = IPAddress.Parse(m_settings.ServerSettings.IPv6Address);
				addresses[1] = IPAddress.Parse(m_settings.ServerSettings.IPv4Address);
				m_tcpClient.Connect(addresses, m_settings.ServerSettings.Port);*/
				m_tcpClient.Connect("localhost", 7234);
			}
		}

		public void Start()
		{
			StartSendThread();
			StartReceiveThread();
			SendInit();
		}

		public void JoinGame(uint gameId, string password)
		{
			SendJoinGame(gameId, password);
		}

		public bool HasJoinedGame()
		{
			return m_data.JoinedGame;
		}

		public void MyAction(Hand.Action action, uint bet)
		{
			NetPacket a = new NetPacketPlayersAction();
			a.Properties.Add(
				NetPacket.PropType.GameState,
				Convert.ToString((int)m_data.CurHand.CurState));
			a.Properties.Add(
				NetPacket.PropType.PlayerAction,
				Convert.ToString((int)action));
			a.Properties.Add(
				NetPacket.PropType.PlayerBet,
				Convert.ToString(bet));
			m_sender.Send(a);
		}

		public void SetTerminateFlag()
		{
			m_sender.SetTerminateFlag();
			m_receiver.SetTerminateFlag();
		}

		public void WaitTermination()
		{
			m_sender.WaitTermination();
			m_receiver.WaitTermination();
		}

		protected void StartReceiveThread()
		{
			m_receiver = new ReceiverThread(m_tcpClient.GetStream(), m_sender, m_data, m_callback);
			m_receiver.Run();
		}

		protected void StartSendThread()
		{
			m_sender = new SenderThread(m_tcpClient.GetStream());
			m_sender.Run();
		}

		protected void SendInit()
		{
			NetPacket init = new NetPacketInit();
			init.Properties.Add(NetPacket.PropType.RequestedVersionMajor, "5");
			init.Properties.Add(NetPacket.PropType.RequestedVersionMinor, "0");
			init.Properties.Add(NetPacket.PropType.PlayerName, m_data.MyName);
			init.Properties.Add(NetPacket.PropType.ServerPassword, "");
			m_sender.Send(init);
		}

		protected void SendJoinGame(uint gameId, string password)
		{
			NetPacket join = new NetPacketJoinGame();
			join.Properties.Add(NetPacket.PropType.GameId, Convert.ToString(gameId));
			join.Properties.Add(NetPacket.PropType.GamePassword, password);
			m_sender.Send(join);
		}

		private TcpClient m_tcpClient;
		private ReceiverThread m_receiver;
		private SenderThread m_sender;
		private Settings m_settings;
		private PokerTHData m_data;
		private ICallback m_callback;
	}
}