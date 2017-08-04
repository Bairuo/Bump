﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassDown : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerController>().MassLevel > 1)
            {
                collision.gameObject.GetComponent<PlayerController>().MassLevel--;
            }
            Destroy(this.gameObject);
        }
    }
}