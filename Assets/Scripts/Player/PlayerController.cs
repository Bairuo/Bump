﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    GameObject camera;

    // 可更改属性
    public int status = 0;
    public float health;
    public float speed = 1;
    public float brake = 0;     // 刹车系数

    // 固定参数
    public float brakeAngle = 60;


    // 其它变量
    public string PlayerID = "0";


    float now_angle = 0;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerID != Client.instance.playerid) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 ClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(transform.position, ClickPos);

            // 加速
            Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
            Vector2 SpeedForce = (Vector2)(ClickPos - transform.position);

            // 由于没有刹车操作，自动判断刹车

            if (Vector2.Angle(velocity, SpeedForce) > brakeAngle)      
            {
                GetComponent<Rigidbody2D>().velocity *= brake;
            }
            GetComponent<Rigidbody2D>().AddForce(SpeedForce * speed);

        }
	}

    // 根据速度自动旋转
    void LateUpdate()
    {
        Vector2 up = new Vector2(0, 1);
        Vector2 left = new Vector2(-1, 0);
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        float angle = Vector2.Angle(up, velocity);

        if (Vector2.Dot(left, velocity) < 0)
        {
            angle = -angle;
        }

        if (velocity != new Vector2(0, 0))
        {
            now_angle = Mathf.Lerp(now_angle, angle, 3 * Time.deltaTime);
            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0, 0, 1), now_angle);
        }

        if (PlayerID == Client.instance.playerid)
        {
            camera.transform.rotation = camera.GetComponent<CameraController>().rotation;
        }
        
    }

    // 关键操作
    public void Dealth()
    {
        if (PlayerID != Client.instance.playerid) return;
        Client.instance.SendPlayerDestroy(PlayerID);
        transform.DetachChildren();

        this.gameObject.SetActive(false);
    }

    public void PlayerDestroy()
    {
        Client.instance.posmanager.PlayerLogoff(PlayerID);
        Destroy(this.gameObject);

        // 胜利检验
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 1)
        {
            Client.instance.SendFail();
            Victory();
        }

    }

    private void Victory()
    {

    }

    // 属性更改
    public void ChangeBrake(float brake)
    {
        if (PlayerID != Client.instance.playerid) return;
        Client.instance.SendChangeBrake(PlayerID, brake);
        RealChangeBrake(brake);
    }
    public void ChangeSpeed(float speed)
    {
        if (PlayerID != Client.instance.playerid) return;
        Client.instance.SendChangeSpeed(PlayerID, speed);
        RealChangeSpeed(speed);
    }
    public void ChangeHealth(float health)      // 生命（参数）小于零时会触发死亡
    {
        if (PlayerID != Client.instance.playerid) return;
        if (health > 0)
        {
            Client.instance.SendChangeHealth(PlayerID, health);
            RealChangeHealth(health);
        }
        else
        {
            Dealth();
        }
        
    }
    public void ChangeStatus(int status)
    {
        if (PlayerID != Client.instance.playerid) return;
        Client.instance.SendChangeStatus(PlayerID, status);
        RealChangeStatus(status);
    }

    public void RealChangeBrake(float brake)
    {
        this.brake = brake;
    }
    public void RealChangeSpeed(float speed)
    {
        this.speed = speed;
    }
    public void RealChangeHealth(float health)
    {
        this.health = health;
    }
    public void RealChangeStatus(int status)
    {
        this.status = status;
    }
}
