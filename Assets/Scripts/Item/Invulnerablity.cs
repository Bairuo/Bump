﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invulnerablity : MonoBehaviour {
    public float time;
    private GameObject obj;
    private GameObject childObj;
    public bool _tag = true;
    void Start()
    {
        childObj = this.gameObject.transform.Find("Inv").gameObject;
        childObj.GetComponent<InvController>().InvInit();
        childObj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        obj = collision.gameObject;
        if (obj.tag == "Player")
        {
            if(!obj.transform.Find("Inv"))
            {
                childObj.SetActive(true);
                if(_tag)
                {
                    childObj.GetComponent<InvController>().InvStart(obj, time);
                    _tag = false;
                }
            }
            else
            {
                obj.transform.Find("Inv").gameObject.GetComponent<InvController>().SetTimer(time);
            }
            Destroy(this.gameObject);
        }
    }

}
