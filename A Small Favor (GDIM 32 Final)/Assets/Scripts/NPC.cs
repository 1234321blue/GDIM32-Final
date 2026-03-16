using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum Mood {happy, fine, mad}
public class NPC : MonoBehaviour
{
    //[SerializeField] private Transform npcTransform;
    [SerializeField] private float checkRad = 1f;
    [SerializeField] private float checkDistance = 1f;
    public Dialogue startingDialogue;
    [SerializeField] private Dialogue waitingDialogue;
    [SerializeField] private DialogueUI dialogueUI;
    //[SerializeField] private PlayerController player;
    public GameObject interactableText;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI tutorialText2;
    [SerializeField] private GameObject keybindText1;
    [SerializeField] private GameObject keybindText2;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip cueSong;
    private Mood npcMood;
    private int interactionChain = 0;
    public Dialogue currentNode;
    private int currentLine = 0;
    private bool runningDialogue;
    private bool waitingForPlayerResponse;

    private Animator animator;
    private bool hasTalked=false;
    public bool hasItem=false;


    void Start()
    {
        currentNode = startingDialogue;
        animator = GetComponentInChildren<Animator>();
        tutorialText.text = "Talk to NPC";
    }

    void Update()
    {
        if(hasItem==false&&hasTalked==true)
        {
            startingDialogue = waitingDialogue;
        }
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        RaycastHit hit;
        bool playerThere = Physics.SphereCast(transform.position, checkRad,transform.forward, out hit, checkDistance);
        if (playerThere)
        {
            if (hit.collider.CompareTag("Player"))
            {
                if(!waitingForPlayerResponse && Input.GetKeyDown(KeyCode.Space))
                {
                    interactableText.SetActive(false);
                    tutorialText.enabled=false;
                    keybindText1.SetActive(false);
                    keybindText2.SetActive(false);
                    AdvanceDialogue();
                    hasTalked=true;
                }
                else if(!runningDialogue)
                {
                    interactableText.SetActive(true);
                    tutorialText.enabled=true;
                    tutorialText2.enabled=true;
                    if(hasTalked)
                    {
                        keybindText1.SetActive(true);
                        keybindText2.SetActive(true); 
                        tutorialText.text = "Give Milk to NPC"; 
                    }
                }
            }
        }
        else
        {
            interactableText.SetActive(false);
            tutorialText.enabled=true;
            tutorialText2.enabled=true;
            if(interactionChain>0)
            {
                keybindText1.SetActive(true);
                keybindText2.SetActive(true);   
            }
        }
    }
        public void AdvanceDialogue ()
    {
        hasItem = false;
        runningDialogue = true;
        animator.SetBool("isTalking", true);
        if(runningDialogue)
        {
            Locator.Instance.player.enabled=false;
            Cursor.lockState = CursorLockMode.None;
            interactableText.SetActive(false);
        }

        if(currentLine < currentNode.npcDialogue.Length)
        {
            // if we still have NPC lines left, keep playing NPC lines
            /*dialogueUI.ShowDialogue(currentNode.npcDialogue[currentLine]);
            currentLine++;*/

            string line = currentNode.npcDialogue[currentLine];

            if(line == "(cue cheesy trumpet music or that one funny sax song)")
            {
                musicSource.clip = cueSong;
                musicSource.Play();
            }

            dialogueUI.ShowDialogue(line);
            currentLine++;
        }
        else if(currentNode.playerResponses != null && currentNode.playerResponses.Length > 0)
        {
            // show player dialogue options, if there are any
            waitingForPlayerResponse = true;
            dialogueUI.ShowPlayerOptions(currentNode.playerResponses);
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
        Locator.Instance.player.enabled=true;
        Cursor.lockState = CursorLockMode.Locked;
        animator.SetBool("isTalking", false);
        interactionChain++;
        musicSource.Stop();
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
        Gizmos.DrawWireSphere(transform.position + transform.forward * checkDistance, checkRad);
    }
}
