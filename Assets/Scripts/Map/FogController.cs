﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : ExLocalAttachment
{
	public float outerRadius;
	public float innerRadius;
	
	public float rec;
	public float t;
	
	public float begina;
	SpriteRenderer rd;
	Material mat;
	
	public GameObject attachment;
	
	protected override void Begin(GameObject x)
	{
		attachment = x;
		rd = this.gameObject.GetComponent<SpriteRenderer>();
		mat = rd.material;
		begina = GetAlpha(rd);
	}
	
	public void MakeVisible(float time)
	{
		rec = t = time;
	}
	
	protected virtual void FixedUpdate()
	{
		if(!attachmentInited) return;
		
		t -= Time.fixedDeltaTime;
		
		if(t <= 0f)
		{
			t = 0f;
			rec = 0f;
		}
		
		// drawing porcess...
		mat.SetFloat("_OuterRadius", outerRadius);
		mat.SetFloat("_InnerRadius", innerRadius);
		mat.SetFloat("_locx", this.gameObject.transform.position.x);
		mat.SetFloat("_locy", this.gameObject.transform.position.y);
		mat.SetVector("_Color", rd.color);
		
		if(t == 0f)
		{
			SetAlpha(rd, 1.0f);
		}
		else
		{
			SetAlpha(rd, ((rec - t) / rec) * begina);
		}
		
		this.gameObject.transform.position = attachment.gameObject.transform.position;
	}
	
	float GetAlpha(SpriteRenderer rd)
	{
		return rd.color.a;
	}
	
	void SetAlpha(SpriteRenderer rd, float v)
	{
		Color c = rd.color;
		rd.color = new Color(c.r, c.g, c.b, v);
	}
	
}
