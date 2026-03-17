using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : Item
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("npc"))
        {
            Locator.Instance.npc.hasMilk=true;
            Locator.Instance.npc.MoodIndication();
        }
        base.OnCollisionEnter(collision);
    }
}
