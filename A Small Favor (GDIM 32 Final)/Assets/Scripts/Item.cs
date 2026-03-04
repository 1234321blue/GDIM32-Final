using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool held;
    [SerializeField] protected Dialogue uniqueDialogue;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("npc"))
        {
        Locator.Instance.npc.interactableText.SetActive(false);
        Locator.Instance.npc.startingDialogue = uniqueDialogue;
        //Locator.Instance.npc.AdvanceDialogue();
        Destroy(gameObject);
        }
    }
}
