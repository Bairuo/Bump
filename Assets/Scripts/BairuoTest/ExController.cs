﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExController : MonoBehaviour
{
    NetObject NetObject;
    public float Health = 10;
    public int Damage = 10;
    float lastTime = 0;
    
    
	void Start () {
        NetObject = GetComponent<NetObject>();

        // 应保证所有的回调都已提前注册
        // 建议在Start里面完成
        NetObject.AddListener("Example", CallBack);

	}

    void Update()
    {
        if (Time.time - lastTime > 0.65f)
        {
            //Debug.Log(i++);
            lastTime = Time.time;
            if (!Client.IsRoomServer())
            {
                foo();
            }
        }

    }


    public void foo()
    {
        Damage++;
        Debug.Log("foo damage " + Damage);
        ProtocolBytes proto = NetObject.GetObjectProtocol();
        
        // 需首先加入协议名，与注册名保持一致
        proto.AddName("Example");

        // 自定变量顺序和类型
        proto.AddInt(Damage);
        proto.AddFloat(Health);

        // 发送
        Debug.Log("Send Name " + proto.GetName());
        NetObject.Send(proto);
    }

    public void CallBack(ProtocolBase protocol)
    {
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;

        // 获取（去掉）协议名
        proto.GetString(start, ref start);

        // 自定义的顺序和类型
        Damage = proto.GetInt(start, ref start);
        Health = proto.GetFloat(start, ref start);

    }
}
