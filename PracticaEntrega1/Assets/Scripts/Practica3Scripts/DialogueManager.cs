using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Animator dialogueAnimator;

    public static DialogueManager Instance;

    private DialogueNode _currentNode;

    private GameObject _talker;

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI SpeechText;
    public TextMeshProUGUI[] OptionsText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartDialogue(Conversation conversation, GameObject talker)
    {
        _talker = talker;
        _currentNode = conversation.StartNode;
        NameText.text = conversation.Name;
        SetNode(_currentNode);
        ShowDialogue();
    }

    private void SetNode(DialogueNode currentNode)
    {
        _currentNode = currentNode;

        if (_currentNode is EndNode)
        {
            DoEndNode(_currentNode as EndNode);
            return;
        }

        SpeechText.text = currentNode.Text;

        for (int i = 0; i < OptionsText.Length; i++)
        {
            Button optionButton = OptionsText[i].transform.parent.GetComponent<Button>();

            if (i < currentNode.Options.Count)
            {
                OptionsText[i].transform.parent.gameObject.SetActive(true);
                OptionsText[i].text = currentNode.Options[i].Text;

                optionButton.onClick.RemoveAllListeners();

                int optionIndex = i;
                optionButton.onClick.AddListener(() => OnOptionClicked(optionIndex));
            }
            else
            {
                OptionsText[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void OnOptionClicked(int optionIndex)
    {
        if (optionIndex < _currentNode.Options.Count)
        {
            DialogueOption selectedOption = _currentNode.Options[optionIndex];
            SetNode(selectedOption.NextNode);
        }
    }

    private void DoEndNode(EndNode currentNode)
    {
        currentNode.OnChosen(_talker);
        HideDialogue();
    }

    private void ShowDialogue()
    {
        dialogueAnimator.SetBool("Show", true);
    }

    public void HideDialogue()
    {
        dialogueAnimator.SetBool("Show", false);
    }
}