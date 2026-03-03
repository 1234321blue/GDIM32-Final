using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mood {happy, fine, mad}
public class NPC : MonoBehaviour
{
    [SerializeField] private Transform npcTransform;
    [SerializeField] private float checkRad = 1f;
    [SerializeField] private float checkDistance = 1f;
    public Mood npcMood;
    public int interactionChain;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] 

    void Start()
    {
        
    }

    void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        RaycastHit hit;
        bool playerThere = Physics.SphereCast(npcTransform.position, checkRad,npcTransform.forward, out hit, checkDistance);
        if (playerThere)
        {
            if (interactionChain==0)
            {
                
            }
            else if (interactionChain==1)
            {
                
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(npcTransform.position + npcTransform.forward * checkDistance, checkRad);
    }
}
