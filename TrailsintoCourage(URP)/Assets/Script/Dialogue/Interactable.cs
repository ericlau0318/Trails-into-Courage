using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public float interactionDistance = 3f;
    public DialogueManager dialogueManager;
    public int sentencesNumber = 0;
    public int choice2Number = 0;
    public int sentenceChoiceNumber;
    public string choice1Text;
    public string choice2Text;
    public Dialogue dialogue;
    public float sphereRadius;
    public bool hasInteracted = false;
    public Animator dialogueAnimator;

    public bool isPlayerInZone = false;
    public GameObject dialogueBox;
    public static bool choiceMode = false;
    private bool allowRepeatedChoices;
    public bool hasCompletedDialogue = false;

    public Animator GirlAnimator;
    public bool isGirlTalking;
    public  GameObject ShopPanel;
    public bool showShopPanelAfterDialogue = false;
    public string uniqueID;
    public bool isKnight;
    public int knightValue;
    private void Awake()
    {
        uniqueID = GenerateUniqueIDBasedOnProperties();
        StartNewGame();
    }
    private string GenerateUniqueIDBasedOnProperties()
    {
        var hash = (gameObject.name + transform.position.ToString()).GetHashCode();
        return hash.ToString();
    }
    public void SaveState()
    {
        PlayerPrefs.SetInt(uniqueID + "_choiceMode", choiceMode ? 1 : 0);
        PlayerPrefs.SetInt(uniqueID + "_completedDialogue", hasCompletedDialogue ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void StartNewGame()
    {
        foreach (var npc in FindObjectsOfType<Interactable>())
        {
            //npc.choiceMode = false;
            npc.hasCompletedDialogue = false;
            npc.SaveState();  // Save these initial states
        }
    }
    public void LoadState()
    {
        choiceMode = PlayerPrefs.GetInt(uniqueID + "_choiceMode", 0) == 1;
        hasCompletedDialogue = PlayerPrefs.GetInt(uniqueID + "_completedDialogue", 0) == 1;
    }

    void Update()
    {
        bool playerCurrentlyInZone = Physics.CheckSphere(transform.position, sphereRadius, LayerMask.GetMask("Player"));
        if (playerCurrentlyInZone)
        {
            dialogueBox.SetActive(true);
        }
        if (gameObject.tag == "Girl" && !playerCurrentlyInZone)
        {
            showShopPanelAfterDialogue = true;
            ShopPanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerCurrentlyInZone && !isPlayerInZone && !hasInteracted)
            {
                if (gameObject.tag == "Knight")
                {
                    //isKnight = true;
                    if (isKnight == true)
                    {
                        dialogueManager.LoadFinishSentense(dialogue);
                        choiceMode = true;
                    }
                }
                else if (gameObject.tag == "Girl")
                {
                    //knightValue = 0;
                    isKnight = false;
                    choiceMode = false;
                    GirlAnimator.SetTrigger("Talking");
                    hasCompletedDialogue = false;
                }
                else
                {
                    //knightValue = 0;
                    isKnight = false;
                    choiceMode = false;
                }
                PlayerController.isPlayerTalking = true;
                dialogueManager.StartDialogue(dialogue, this);
                hasInteracted = true;
            }
            if (!playerCurrentlyInZone)
            {
                //Debug.Log(playerCurrentlyInZone);
                hasInteracted = false;
                isPlayerInZone = false;
            }
            isPlayerInZone = playerCurrentlyInZone;
        }
        if (hasInteracted && !playerCurrentlyInZone)
        {
            CloseDialogue();
        }
    }

    public bool LoadChoiceState()
    {
        //SaveState();
        return isKnight = true;
    }
    void CloseDialogue()
    {
        dialogueAnimator.SetBool("IsOpen", false);
        hasInteracted = false;
        isPlayerInZone = false;
        PlayerController.isPlayerTalking = false;
        dialogueManager.awaitingChoice = false;
    }
    /*void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player GameObject has the tag "Player"
        {
            Debug.Log("123");
            dialogueManager.StartDialogue(dialogue);

        }
    }*/
    void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sphereRadius);
        }
         void ResetInteraction()
        {
            hasInteracted = false;
        }
}

    
    /*void CheckForInteraction()
    {
        Debug.Log("Checking for interaction");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
                     Debug.Log("123");
        }
        else
        {
            Debug.Log("Raycast did not hit");
        }
    }*/

