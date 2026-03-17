using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public bool held;
    [SerializeField] protected Dialogue uniqueDialogue;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("npc"))
        {
        Locator.Instance.npc.interactableText.SetActive(false);
        Locator.Instance.npc.hasItem=true;
        Locator.Instance.npc.currentNode = uniqueDialogue;
        Locator.Instance.npc.startingDialogue = uniqueDialogue;
        Destroy(gameObject);
        }
    }
}
