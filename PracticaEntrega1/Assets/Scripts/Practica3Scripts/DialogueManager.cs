using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    //Referencia al componente Animator que controla las animaciones
    public Animator dialogueAnimator;
    public static DialogueManager Instance;
    //Nodo actual del dialogo
    private DialogueNode _currentNode;
    //GameObject que está hablando
    private GameObject _talker;
    //Texto para mostrar el nombre del NPC
    public TextMeshProUGUI NameText;
    //Texto para mostrar el parlamento
    public TextMeshProUGUI SpeechText;
    //Array con textos para las opciones de diálogo
    public TextMeshProUGUI[] OptionsText;
    //Botón para cerrar el diálgo
    public Button closeButton;
    //Referencia a la imagen de fondo 
    public Image backgroundImage;
    //Fuente de audio para reproducir el sonido de los EndNodes
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
        //Se añade el boton de cierre del diálogo
        closeButton.onClick.AddListener(HideDialogue);
    }

    public void StartDialogue(Conversation conversation, GameObject talker)
    {
        _talker = talker;
        //Asigna el nodo inicial de la primera conversación
        _currentNode = conversation.StartNode;
        //Nombre de la conversación
        NameText.text = conversation.Name;
        // Pasar el color de fondo del primer nodo
        SetNode(_currentNode, conversation.FirstNodeBackgroundColor); 
        //Muestra el dialogo
        ShowDialogue();
    }
    //Configuaración del nodo de dialogo
    private void SetNode(DialogueNode currentNode, Color backgroundColor)
    {
        _currentNode = currentNode;
        //Si el nodo es el último, se desarrolla el metodo DoEndNode
        if (_currentNode is EndNode)
        {
            DoEndNode(_currentNode as EndNode);
            return;
        }

        SpeechText.text = currentNode.Text;
        // Cambiar el color de fondo
        ChangeBackgroundColor(backgroundColor); 

        for (int i = 0; i < OptionsText.Length; i++)
        {
            Button optionButton = OptionsText[i].transform.parent.GetComponent<Button>();

            if (i < currentNode.Options.Count)
            {
                //Activa el botón de opción
                OptionsText[i].transform.parent.gameObject.SetActive(true);
                //Mustra el texto de la opción
                OptionsText[i].text = currentNode.Options[i].Text;

                optionButton.onClick.RemoveAllListeners();

                int optionIndex = i;
                optionButton.onClick.AddListener(() => OnOptionClicked(optionIndex));
            }
            else
            {
                //desactiva el botón di no hay opción correspondiente
                OptionsText[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void OnOptionClicked(int optionIndex)
    {
        if (optionIndex < _currentNode.Options.Count)
        {
            DialogueOption selectedOption = _currentNode.Options[optionIndex];
            // Pasar el color de fondo de la opción seleccionada
            SetNode(selectedOption.NextNode, selectedOption.BackgroundColor); 
        }
    }
    //Cambio de color de fondo
    private void ChangeBackgroundColor(Color color)
    {
        backgroundImage.color = color;
    }
    //Método final
    private void DoEndNode(EndNode currentNode)
    {
        currentNode.OnChosen(_talker);
        //Hace sonar el audio
        PlayEndNodeAudio(currentNode.endAudio);
        //Esconde el dialogo
        HideDialogue();
    }
    //Suena el audio
    private void PlayEndNodeAudio(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    //Mostrar diálogo (activo boleana show)
    private void ShowDialogue()
    {
        dialogueAnimator.SetBool("Show", true);
    }
    //Escondo diálogo (activo boleana hide)
    public void HideDialogue()
    {
        dialogueAnimator.SetBool("Show", false);
        // Reiniciar el nodo actual para comenzar desde el principio
        _currentNode = null;
        // Reiniciar el GameObject que está hablando
        _talker = null; 
    }
}