﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassDown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /// Try something interesting , may remove or re-design.
        if(collision.gameObject.tag == "Player")
        {
            var c = collision.gameObject.GetComponent<BuffMassUp>();
            if(c != null)
            {
                Destroy(c);
                Destroy(this.gameObject);
            }
        }
    }
}
