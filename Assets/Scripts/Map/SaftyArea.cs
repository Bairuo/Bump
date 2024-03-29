﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaftyArea : MonoBehaviour
{
	public float radius;
	public float maxRadius;
	public float circleWidth;
	public float decreasePerSec;
	public float damagePerSec;
	// public float minRadius;
	float initRadius;

	SpriteRenderer rd;
	Material rmt;
		
	public int InvBit = 0x1;

    public static SaftyArea instance;

    public SaftyArea()
    {
        instance = this;
    }

	void Start()
	{
		radius = maxRadius;
		rd = this.gameObject.GetComponent<SpriteRenderer>();
		rmt = rd.material;
	}
	
	void Update()
	{

        if (Client.IsRoomServer()) radius -= decreasePerSec * Time.deltaTime;
		// drawing precess...
		rmt.SetFloat("_InnerRadius", radius - circleWidth);
		rmt.SetFloat("_OuterRadius", radius);
		if(radius < 0.0f) radius = 0.0f;
		
		
		foreach(var i in GameObject.FindGameObjectsWithTag("Player"))
		{
			if(Vector2.Distance(i.transform.position, this.gameObject.transform.position) >= radius)
			{
				i.GetComponent<Unit>().health -= Time.deltaTime * damagePerSec;
			}
		}
	}
}
