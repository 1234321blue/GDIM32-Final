using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionItems : Item
{
    [SerializeField] private Dialogue madDialogue;
    protected override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("npc"))
        {

        Locator.Instance.npc.moodIndicator++;

        }
        Locator.Instance.npc.MoodIndication();
        if(Locator.Instance.npc.npcMood==Mood.mad)
        {
            uniqueDialogue = madDialogue;
        }
        base.OnCollisionEnter(collision);
    }
}
