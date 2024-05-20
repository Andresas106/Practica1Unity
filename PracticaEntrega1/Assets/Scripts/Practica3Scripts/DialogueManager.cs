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
    // Start is called before the first frame update
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
        _currentNode = currentNode; // Actualizamos el nodo actual
        SpeechText.text = currentNode.Text;

        for (int i = 0; i < OptionsText.Length; i++)
        {
            Button optionButton = OptionsText[i].transform.parent.GetComponent<Button>();

            if (i < currentNode.Options.Count)
            {
                OptionsText[i].transform.parent.gameObject.SetActive(true);
                OptionsText[i].text = currentNode.Options[i].Text;

                // Eliminamos cualquier listener anterior para evitar múltiples adiciones
                optionButton.onClick.RemoveAllListeners();

                // Capturamos el índice para el evento onClick
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

    private void ShowDialogue()
    {
        dialogueAnimator.SetBool("Show", true);
    }
    public void HideDialogue()
    {
        dialogueAnimator.SetBool("Show", false);
    }
}
