﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenView : MonoBehaviour
{
    public float time;
    void OnTriggerEnter2D(Collider2D x)
    {
        GameObject.Find("fog").GetComponent<FogController>().MakeVisible(time);
    }
    
}
