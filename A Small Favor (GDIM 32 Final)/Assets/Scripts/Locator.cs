using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
public static Locator Instance { get; private set; }
public NPC npc {get; private set;}
public PlayerController player {get; private set;}
private void Awake() 
{
    if (Instance != null && Instance != this) 
    {
        Destroy(this);
        return;
    }
    Instance = this;
    GameObject NPC = GameObject.FindWithTag("npc");
    npc = NPC.GetComponent<NPC>();
    GameObject playerFinder = GameObject.FindWithTag("Player");
    player = playerFinder.GetComponent<PlayerController>();
}
}
