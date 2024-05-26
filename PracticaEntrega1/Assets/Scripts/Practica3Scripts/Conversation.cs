using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Genero una opción en el menú de unity para crear una instancia del ScriptableObject
[CreateAssetMenu(fileName ="NewConversation",menuName ="Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    //Variable que almacena el nombre de la conversación
    public string Name;
    //Variable que almacena el nodo inicial de la conversación
    public DialogueNode StartNode;
    //Variable que guarda el color de fondo del primer nodo
    public Color FirstNodeBackgroundColor;
}
