﻿using System;
using UnityEngine;

public class NetUnitData
{
    //对于玩家来说id是玩家在房间内的id
    //对于其它物体来说id是创建端的InstanceID
    // string id;
    public Vector3 fpos;
    public Vector3 lpos;
    float lastRecvTime = float.MinValue;

    public Vector2 fang;
    public Vector2 lang;

    public float delta = 1;
    public int DATAindex = 0;
    public int LastReceiveID = -1;

    public void Update(Vector3 npos)
    {
        fpos = lpos + (npos - lpos) * 2;
        //fang = lang + (nang - lang) * 2;
        if (Time.time - lastRecvTime > 0.3f)
        {
            fpos = npos;
            //fang = nang;
        }
        delta = Time.time - lastRecvTime;
        
        lpos = npos;
        lastRecvTime = Time.time;
    }

    public NetUnitData(string _id, GameObject obj)
    {
        // id = _id;
        float x = obj.gameObject.transform.position.x;
        float y = obj.gameObject.transform.position.y;
        float z = obj.gameObject.transform.position.z;

        fpos = lpos = new Vector3(x, y, z);
        fang = lang = new Vector2(0, 0);

        if (obj.tag != "Player")
        {
            if (Client.instance.playerid != "0")
            {
                // Body r = obj.GetComponent<Body>();
                // r.constraints = RigidbodyConstraints2D.FreezeAll;
                
            }

        }
        else
        {
            
            //if (id != Client.instance.playerid)
            if (Client.instance.playerid != "0")
            {
                // Body r = obj.GetComponent<Body>();
                // r.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

    }

    public int GetDataID()
    {
        return Sys.GetIndex(ref DATAindex);
    }

    public ProtocolBytes GetUnitData(int DataID, string protoName, string id, Vector3 pos)
    {
        ProtocolBytes proto = new ProtocolBytes();
        proto.AddString(protoName);
        proto.AddInt(DataID);

        proto.AddString(id);

        proto.AddFloat(pos.x);
        proto.AddFloat(pos.y);
        proto.AddFloat(pos.z);

        return proto;
    }

    public ProtocolBytes GetUDPUnitData(int DataID, string protoName, string id, Vector3 pos)
    {
        ProtocolBytes proto = new ProtocolBytes();
        proto.AddInt(Client.instance.conn_id);
        proto.AddString(protoName);
        proto.AddInt(DataID);

        proto.AddString(id);

        proto.AddFloat(pos.x);
        proto.AddFloat(pos.y);
        proto.AddFloat(pos.z);

        return proto;
    }

}