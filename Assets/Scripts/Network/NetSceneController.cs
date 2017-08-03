﻿using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class NetSceneController : MonoBehaviour {
    public Text IPInput; 
    public Text Information;
    int port = 9990;


    void Start()
    {

    }

    public void ClickGame()
    {
        //EnterGame();
    }

    public void EnterGame()
    {
        Application.LoadLevel("BairuoTest");
    }

    public void StartServer()
    {
        if (ServerNet.IsUse() || Client.IsUse())
        {
            return;
        }

        ServerNet server = new ServerNet();
        server.Start(Network.player.ipAddress, port);

        Information.text = "服务器IP： " + Network.player.ipAddress;
    }

    public void StartConnect()
    {
        if (ServerNet.IsUse() || Client.IsUse())
        {
            return;
        }

        Client client = new Client();

        if (client.Connect(IPInput.text, port))
        {
            Information.text = "成功连接服务器";
        }
        else
        {
            Information.text = "加入服务器失败";
        }

    }

    public void DisConnect()
    {

    }
}
