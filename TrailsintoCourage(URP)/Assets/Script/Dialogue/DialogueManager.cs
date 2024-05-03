using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    private Queue<string> sentences;
    public Interactable interactable;
    public bool showAllSentence = false;
    public bool isLastSentence = false; // Flag to check if it's the last sentence
    public bool awaitingChoice = false;

    public Button choiceButton1;
    public Button choiceButton2;

    private Queue<string> choice1Sentences;
    private Queue<string> choice2Sentences;
    private Queue<string> finishSentences;
    public bool isDialogueActive = false;
    private bool FinalChoice = false;

    void Start()
    {
        sentences = new Queue<string>();
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        choice1Sentences = new Queue<string>();
        choice2Sentences = new Queue<string>();
        finishSentences = new Queue<string>();
    }
    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E) && !showAllSentence && !awaitingChoice)
        {
            DisplayNextSentence();
            showAllSentence = true;
        }
    }

    public void StartDialogue(Dialogue dialogue, Interactable npc)
    {
        interactable = npc;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        npc.sentencesNumber = 0;
        sentences.Clear();
        choice1Sentences.Clear();
        choice2Sentences.Clear();
        finishSentences.Clear();
        if (Interactable.choiceMode == true || npc.hasCompletedDialogue || npc.knightValue == 1)
        {
            LoadFinishSentense(dialogue, npc);
        }
        else
        {
            sentences.Clear();
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            choice1Sentences.Clear();
            foreach (string sentence in dialogue.choice1Sentences)
            {
                choice1Sentences.Enqueue(sentence);
            }

            choice2Sentences.Clear();
            foreach (string sentence in dialogue.choice2Sentences)
            {
                choice2Sentences.Enqueue(sentence);
            }
            finishSentences.Clear();
            foreach (string sentence in dialogue.finishSentences)
            {
                finishSentences.Enqueue(sentence);
            }
        }

        choiceButton1.GetComponentInChildren<Text>().text = npc.choice1Text;
        choiceButton2.GetComponentInChildren<Text>().text = npc.choice2Text;
        DisplayNextSentence();
        isDialogueActive = false;
        showAllSentence = true;
    }
    public void LoadFinishSentense(Dialogue dialogue, Interactable npc)
    {
        finishSentences.Clear();
        foreach (string sentence in dialogue.finishSentences)
        {
            sentences.Enqueue(sentence);
        }
        npc.isKnight = true;
    }
    public void DisplayNextSentence()
    {
        /*if (sentences.Count == 0 || choice1Sentences.Count == 0 || choice2Sentences.Count == 0)
        //if (sentences.Count == 0)
        {
            EndDialogue(interactable);
            return;
        }*/
        if (sentences.Count == 0)
        {
            // End the dialogue if there are no sentences left to display.
            EndDialogue(interactable);
            return;
        }
        interactable.sentencesNumber++;
        if (!Interactable.choiceMode && interactable.sentencesNumber >= 0 && interactable.sentencesNumber == interactable.sentenceChoiceNumber && !awaitingChoice)
        {
            choiceButton1.gameObject.SetActive(true);
            choiceButton2.gameObject.SetActive(true);
            awaitingChoice = true;
        }
        else
        {
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
            awaitingChoice = false;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        showAllSentence = false;
        isDialogueActive = true;
    }

    public void EndDialogue(Interactable npc)
    {
        animator.SetBool("IsOpen", false);
        StartCoroutine(CheckPlayerInZoneAfterDelay(1f));
        isDialogueActive = false;
        //npc.hasCompletedDialogue = true;
        interactable.knightValue = 1;
        npc.LoadChoiceState();
        PlayerController.isPlayerTalking = false;
        if (npc.showShopPanelAfterDialogue && npc.sentencesNumber == 1)
        {
            npc.ShopPanel.SetActive(true);
            PlayerController.isPlayerTalking = true;
        }
    }

    private IEnumerator CheckPlayerInZoneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Check if the player is still in the interaction zone
        if (interactable != null && Physics.CheckSphere(interactable.transform.position, interactable.sphereRadius, LayerMask.GetMask("Player")))
        {
            interactable.isPlayerInZone = false;
            interactable.hasInteracted = false;
        }
    }
    public void DisplayButtonSentence()
    {
        if (!showAllSentence && !awaitingChoice)
        {
            DisplayNextSentence();
            showAllSentence = true;
        }
    }

    public void ChooseOption1()
    {
        if (!showAllSentence)
        {
            LoadChoiceSentences(choice1Sentences);
            Interactable.choiceMode = true;
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
            awaitingChoice = false;
            showAllSentence = true;
        }
    }
    public void ChooseOption2()
    {
        if (!showAllSentence && !FinalChoice)
        {
            LoadChoiceSentences(choice2Sentences);
            Interactable.choiceMode = true;
            if (sentences.Count > 0)
            {
                choiceButton1.gameObject.SetActive(false);
                choiceButton2.gameObject.SetActive(true);
            }
            else 
            {
                choiceButton2.gameObject.SetActive(true);
                choiceButton2.GetComponentInChildren<Text>().text = "Yes";
                interactable.choice2Number = 0;
                FinalChoice = true;
            }
            awaitingChoice = true;
            showAllSentence = true;
        }
        else if (FinalChoice) // Check if "Yes" has been selected
        {
            LoadChoiceSentences(choice1Sentences); // Load Choice 1 sentences`
            FinalChoice = false; // Reset the flag
        }
    }
    private void LoadChoiceSentences(Queue<string> choiceSentences)
    {
        if (interactable.choice2Number == 0)
        {
            sentences.Clear();
            foreach (string sentence in choiceSentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
            interactable.choice2Number++;
        }
        else
        {
            DisplayNextSentence();
        }
    }
    public void ClosePanel()
    {
        interactable.ShopPanel.SetActive(false);
        PlayerController.isPlayerTalking = false;
    }
}
