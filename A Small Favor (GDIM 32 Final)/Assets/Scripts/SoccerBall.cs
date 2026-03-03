using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : Item
{
    public override void Use()
    {
        if(held==true)
        {
            npc.startingDialogue=uniqueDialogue;
        }
    }
}
