using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //Detecci�n de jugador cuando entra en el Trigger del NPC
    public Conversation Conversation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Debug.Log("Jugador detectado");
            DialogueManager.Instance.StartDialogue(Conversation, gameObject);
        }
    }
}
