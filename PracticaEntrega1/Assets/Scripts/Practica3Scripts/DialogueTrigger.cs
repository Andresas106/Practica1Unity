using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //Detección de jugador cuando entra en el Trigger del NPC
    public Conversation Conversation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Debug.Log("Jugador detectado");
            DialogueManager.Instance.StartDialogue(Conversation, gameObject);
        }
    }
    //Deteccion de si el jugador sale del area del trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            DialogueManager.Instance.HideDialogue();
        }
    }
}
