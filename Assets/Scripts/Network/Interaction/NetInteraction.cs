﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetInteraction{
    const int port = 9970;
    const string remoteIP = "119.23.52.136";

    static NetInteraction()
    {
        new ServerNet();
        new Client();
    }

    public static string StartLANServer()
    {
        if (ServerNet.IsUse() || Client.IsUse())
        {
            return "客户端或服务器已经运行";
        }

        ServerNet.instance.Start(Network.player.ipAddress, port);
        Client.instance.Connect(Network.player.ipAddress, port);

        return Network.player.ipAddress;
    }

    public static bool StartWANRoom()
    {
        Client.instance.questroom = "-1";
        return ConnectWAN();
    }

    public static bool ConnectWANRoom(string roomID)
    {
        Client.instance.questroom = roomID;
        return ConnectWAN();
    }

    public static bool ConnectWANRadom()
    {
        Client.instance.questroom = "-2";
        return ConnectWAN();
    }

    public static bool ConnectWAN()
    {
        if (ServerNet.IsUse() || Client.IsUse())
        {
            return false;
        }

        return Client.instance.Connect(remoteIP, port);
    }

    public static bool ConnectLANServer(string ip, int port)
    {
        if (ServerNet.IsUse() || Client.IsUse())
        {
            return false;
        }

        Client client = new Client();

        return client.Connect(ip, port);
    }


}