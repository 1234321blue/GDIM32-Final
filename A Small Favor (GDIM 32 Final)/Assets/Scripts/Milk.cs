using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : Item
{
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if(collision.gameObject.CompareTag("npc"))
        {
            
        }
    }
}
