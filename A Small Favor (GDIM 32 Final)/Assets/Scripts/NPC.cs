using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public enum Mood {happy, fine, mad, pillow}
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
    [SerializeField] private GameObject tutorialText2;
    //[SerializeField] private GameObject keybindText1;
    //[SerializeField] private GameObject keybindText2;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject pillowScreen;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip cueSong;
    public Mood npcMood{get; private set;} = Mood.happy; 
    public int moodIndicator = 0;
    private int interactionChain = 0;
    public Dialogue currentNode;
    private int currentLine = 0;
    private bool runningDialogue;
    private bool waitingForPlayerResponse;

    private Animator animator;
    private bool hasTalked=false;
    public bool hasItem=false;
    public bool hasMilk = false;


    void Start()
    {
        currentNode = startingDialogue;
        animator = GetComponentInChildren<Animator>();
        tutorialText.text = "- Talk to NPC";
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
                    tutorialText2.SetActive(false);
                    //keybindText1.SetActive(false);
                    //keybindText2.SetActive(false);
                    AdvanceDialogue();
                    hasTalked=true;
                }
                else if(!runningDialogue)
                {
                    interactableText.SetActive(true);
                    tutorialText.enabled=true;
                    if(hasTalked)
                    {
                        //keybindText1.SetActive(true);
                        //keybindText2.SetActive(true); 
                        tutorialText2.SetActive(true);
                        tutorialText.text = "- Give Milk to NPC"; 
                    }
                }
            }
        }
        else
        {
            interactableText.SetActive(false);
            tutorialText.enabled=true;
            if(interactionChain>0)
            {
                //keybindText1.SetActive(true);
                //keybindText2.SetActive(true);  
                tutorialText2.SetActive(true); 
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

            if(line == "   ")
            {
                npcMood = Mood.pillow;
                EndGame();
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
        if(hasMilk||npcMood==Mood.mad)
        {
            EndGame();
        }
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
    public void MoodIndication()
    {
        if(!hasMilk)
        {
        if(moodIndicator==1)
        {
            npcMood=Mood.happy;
        }
        if(moodIndicator==2)
        {
            npcMood=Mood.fine;
        }
        if(moodIndicator>=3)
        {
            npcMood=Mood.mad;
        }
        }
        else
        {
            npcMood=Mood.happy;
        }
    }
    private void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Locator.Instance.player.enabled=false;
        this.enabled=false;
        interactableText.SetActive(false);
        tutorialText.enabled=false;
        tutorialText2.SetActive(false);
        crosshair.SetActive(false);
        if(npcMood==Mood.mad)
        {
            loseScreen.SetActive(true);
        }
        else if (npcMood==Mood.happy)
        {
            winScreen.SetActive(true);
        }
        else if(npcMood==Mood.pillow)
        {
            pillowScreen.SetActive(true);
        }
    }
}
