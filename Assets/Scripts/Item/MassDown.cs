﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassDown : ExNetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Client.IsRoomServer()) return;
        
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
