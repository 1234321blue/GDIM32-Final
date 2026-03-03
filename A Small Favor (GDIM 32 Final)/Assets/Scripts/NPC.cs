using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mood {happy, fine, mad}
public class NPC : MonoBehaviour
{
    [SerializeField] private Transform npcTransform;
    [SerializeField] private float checkRad = 1f;
    [SerializeField] private float checkDistance = 1f;
    [SerializeField] private Dialogue startingDialogue;
    [SerializeField] private DialogueUI dialogueUI;
    public Mood npcMood;
    public int interactionChain;
    private Dialogue currentNode;
    private int currentLine = 0;
    private bool runningDialogue;
    private bool waitingForPlayerResponse;

    void Start()
    {
        currentNode = startingDialogue;
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
                if(!waitingForPlayerResponse && Input.GetKeyDown(KeyCode.Space))
                {
                    AdvanceDialogue();
                }
            
                else
                {
                    EndDialogue();
                }
            }

        else if (interactionChain==1)
        {
                
        }
        }
    }
        private void AdvanceDialogue ()
    {
        runningDialogue = true;

        if(currentLine < currentNode.npcDialogue.Length)
        {
            // if we still have NPC lines left, keep playing NPC lines
            dialogueUI.ShowDialogue(currentNode.npcDialogue[currentLine]);
            currentLine++;
        }
        else if(currentNode.playerResponses != null && currentNode.playerResponses.Length > 0)
        {
            // show player dialogue options, if there are any
            waitingForPlayerResponse = true;
            dialogueUI.ShowPlayerOptions();
        }
        else 
        {
            // if there are no NPC or player lines left, close dialogue UI
            EndDialogue();
        }
    }

    private void EndDialogue ()
    {
        runningDialogue = false;
        waitingForPlayerResponse = false;
        currentNode = startingDialogue;
        currentLine = 0;
        dialogueUI.HideDialogue();
    }

    public void SelectedOption(int option)
    {
        currentLine = 0;
        waitingForPlayerResponse = false;

        currentNode = currentNode.npcReplies[option];
        AdvanceDialogue();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(npcTransform.position + npcTransform.forward * checkDistance, checkRad);
    }
}
