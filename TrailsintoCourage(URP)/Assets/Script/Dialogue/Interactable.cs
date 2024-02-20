using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool choiceMode = false;
    private bool allowRepeatedChoices;
    public bool hasCompletedDialogue = false;
    void Start()
    {
        dialogueAnimator = GameObject.Find("DialogueBox").GetComponent<Animator>();
    }
    void Update()
    {
        bool playerCurrentlyInZone = Physics.CheckSphere(transform.position, sphereRadius, LayerMask.GetMask("Player"));
        if (playerCurrentlyInZone)
        {
            dialogueBox.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerCurrentlyInZone && !isPlayerInZone && !hasInteracted)
            {
                PlayerController.isPlayerTalking = true;
                dialogueManager.StartDialogue(dialogue, this);
                hasInteracted = true;
            }
            if (!playerCurrentlyInZone)
            {
                Debug.Log(playerCurrentlyInZone);
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

