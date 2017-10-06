﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetObject : MonoBehaviour {
    //  网络物体必须创造器动态创建
    // NetID一经赋值不得修改
    public string NetID
    {
        get
        {
            return NetID;
        }
        private set
        {
            if (NetID == null)
            {
                NetID = value;
                SynSystem.Register(NetID, this);
            }
        }
    }  
    public ProtocolBytes Protocol;
    ProtocolBytes UserProtocol;
    Dictionary<string, Synable> SynVars = new Dictionary<string, Synable>();
    List<Synable> Changes = new List<Synable>();

    public delegate void Delegate(ProtocolBase protocol);
    private Dictionary<string, Delegate> EventDict = new Dictionary<string, Delegate>();
    private Dictionary<string, Delegate> OnceDict = new Dictionary<string, Delegate>();

    public void ObjectRegister(string id)
    {
        NetID = id;
        //Client.instance.AddListener(SynSystem.VarFlag + id, CallNetObject);
    }

    public void CallNetObject(ProtocolBase protocol)
    {
        ProtocolBytes proto = (ProtocolBytes)protocol;
        string name = proto.GetName();


    }

    public void VarRegister(string id, Synable a)
    {
        SynVars.Add(id, a);
    }

    public void ChangeVar(Synable a)
    {
        Changes.Add(a);
    }

    void GetNewProtocol()
    {
        Protocol = new ProtocolBytes();
        Protocol.AddString(SynSystem.VarFlag + NetID);
    }

    public void Send()
    {
        lock (Protocol)
        {
            Client.instance.Send(Protocol);
            GetNewProtocol();
        }
    }

    // 用户自行构造协议和定义顺序
    // 初始协议必须由GetObjectProtocol获得

    public ProtocolBytes GetObjectProtocol()
    {
        UserProtocol = new ProtocolBytes();
        UserProtocol.AddString(SynSystem.SystemUserFlag);
        UserProtocol.AddString(NetID);

        return UserProtocol;
    }


    public void Send(ProtocolBase protocol)
    {
        Client.instance.Send(protocol);
    }

    public void DispatchMsgEvent(ProtocolBase protocol)
    {
        if (protocol == null) return;
        string name = protocol.GetName();

        if (EventDict.ContainsKey(name))
        {
            EventDict[name](protocol);
            return;
        }

        if (OnceDict.ContainsKey(name))
        {
            OnceDict[name](protocol);
            OnceDict[name] = null;
            OnceDict.Remove(name);
            return;
        }
    }

    public void AddListener(string name, Delegate cb)
    {
        if (EventDict.ContainsKey(name))
        {
            EventDict[name] += cb;
        }
        else
        {
            EventDict[name] = cb;
        }
    }

    public void AddOnceListener(string name, Delegate cb)
    {
        if (OnceDict.ContainsKey(name))
        {
            OnceDict[name] += cb;
        }
        else
        {
            OnceDict[name] = cb;
        }
    }

    public void DelListener(string name, Delegate cb)
    {
        if (EventDict.ContainsKey(name))
        {
            EventDict[name] -= cb;
            if (EventDict[name] == null)
                EventDict.Remove(name);
        }
    }
    public void DelOnceListener(string name, Delegate cb)
    {
        if (OnceDict.ContainsKey(name))
        {
            OnceDict[name] -= cb;
            if (OnceDict[name] == null)
                OnceDict.Remove(name);
        }
    }


}

/* 关于网络物体创建与ID分配的记录 */
// 一般障碍物创建时没有使用网络ID，且由Host创建后再去创建另外一个
// 玩家由创建器GenerateController创建和分配ID
// 物体由创建器ObjectGenerator创建