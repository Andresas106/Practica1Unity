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
    public Button closeButton;
    public Image backgroundImage;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        closeButton.onClick.AddListener(HideDialogue);
    }

    public void StartDialogue(Conversation conversation, GameObject talker)
    {
        _talker = talker;
        _currentNode = conversation.StartNode;
        NameText.text = conversation.Name;
        SetNode(_currentNode, conversation.FirstNodeBackgroundColor); // Pasar el color de fondo del primer nodo
        ShowDialogue();
    }

    private void SetNode(DialogueNode currentNode, Color backgroundColor)
    {
        _currentNode = currentNode;

        if (_currentNode is EndNode)
        {
            DoEndNode(_currentNode as EndNode);
            return;
        }

        SpeechText.text = currentNode.Text;
        ChangeBackgroundColor(backgroundColor); // Cambiar el color de fondo

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
            SetNode(selectedOption.NextNode, selectedOption.BackgroundColor); // Pasar el color de fondo de la opción seleccionada
        }
    }

    private void ChangeBackgroundColor(Color color)
    {
        backgroundImage.color = color;
    }

    private void DoEndNode(EndNode currentNode)
    {
        currentNode.OnChosen(_talker);
        PlayEndNodeAudio(currentNode.endAudio);
        HideDialogue();
    }

    private void PlayEndNodeAudio(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
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