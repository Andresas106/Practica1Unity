
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    //Variable que almacena el texto del nodo del diálogo
    public string Text;
    //Variable que almacena las opciones de dialogo
    public List<DialogueOption> Options;
}

[System.Serializable]
public class DialogueOption
{
    //Variable que almacena el texto de la opcion
    public string Text;
    //Almacena el siguiente nodo
    public DialogueNode NextNode;
    //Almacena el color de fondo a elegir según el estado animico del NPC
    public Color BackgroundColor; 
}