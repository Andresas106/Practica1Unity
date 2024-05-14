using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialodManagment : MonoBehaviour
{
    public Animator dialogAnimator;
    public static DialodManagment Instance;
    private DialogueNode _currentNode;
    private GameObject _talker;
    public TextMeshProUGUI nameText;
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

    public void StartDialog(Conversation conversation, GameObject talker)
    {
        _talker = talker;
        _currentNode = conversation.StarNode;
        nameText.text = conversation.Name;
        SetNode(_currentNode);
        showDialog();
    }

    private void SetNode(DialogueNode currentNode)
    {
        SpeechText.text = currentNode.text;
        //Ahora a casa botón le tenemos que poner sus opciones y si no desactivarlos. 
        for (int i = 0; i < OptionsText.Length; i++)
        {
            if (i < currentNode.Options.Count)
            {
                OptionsText[i].transform.parent.gameObject.SetActive(true);
                OptionsText[i].text = currentNode.Options[i].text;
            }
            else
            {
                OptionsText[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void showDialog()
    {
        dialogAnimator.SetBool("Show", true);
    }
    public void HideDialog()
    {
        dialogAnimator.SetBool("Hide", true);
    }
}