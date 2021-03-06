﻿/* 
 * Starrybound Server
 * Copyright 2013, Avilance Ltd
 * Created by Zidonuke (zidonuke@gmail.com) and Crashdoom (crashdoom@avilance.com)
 * 
 * This file is a part of Starrybound Server.
 * Starrybound Server is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * Starrybound Server is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with Starrybound Server. If not, see http://www.gnu.org/licenses/.
*/

using com.avilance.Starrybound.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.avilance.Starrybound.Commands
{
    class Players : CommandBase
    {
        public Players(ClientThread client)
        {
            this.name = "players, /list, /who";
            this.HelpText = "Lists all of the players connected to the server.";

            this.client = client;
            this.player = client.playerData;
        }

        public override bool doProcess(string[] args)
        {
            string list = "";

            int noOfUsers = StarryboundServer.clients.Count;
            int i = 0;

            foreach (string user in StarryboundServer.clients.Keys) {
                list = list + user;

                if (i != noOfUsers - 1) list = list + ", ";

                i++;
            }

            Packet11ChatSend packet = new Packet11ChatSend(this.client, false, Util.Direction.Client);
            packet.prepare(Util.ChatReceiveContext.White, "", 0, "server", noOfUsers + "/" + StarryboundServer.config.maxClients + " player(s): " + list);
            packet.onSend();

            return true;
        }
    }
}
