using System.Collections;
using System.Collections.Generic;
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
        if (interactable.choiceMode == true)
        {
            foreach (string sentence in dialogue.finishSentences)
            {
                sentences.Enqueue(sentence);
            }
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
        }

        choiceButton1.GetComponentInChildren<Text>().text = npc.choice1Text;
        choiceButton2.GetComponentInChildren<Text>().text = npc.choice2Text;
        DisplayNextSentence();
        isDialogueActive = false;
        showAllSentence = true;
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 || choice1Sentences.Count == 0 || choice2Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        interactable.sentencesNumber++;
        if (!interactable.choiceMode && interactable.sentencesNumber >= 0 && interactable.sentencesNumber == interactable.sentenceChoiceNumber && !awaitingChoice)
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

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        StartCoroutine(CheckPlayerInZoneAfterDelay(1f));
        isDialogueActive = false;
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
            interactable.choiceMode = true;
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
            awaitingChoice = false;
            showAllSentence = true;
        }
    }
    public void ChooseOption2()
    {
        if (!showAllSentence)
        {
            LoadChoiceSentences(choice2Sentences);
            interactable.choiceMode = true;
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
            LoadChoiceSentences(choice1Sentences); // Load Choice 1 sentences
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

}