using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewNode", menuName = "Dialog/Node")]
public class DialogueNode : ScriptableObject
{
    public string text;
    public List<DialogueNode> Options;
}
//Con esto hacemos que se pueda poner mas de una respuesta
[System.Serializable]
public class DialogueOption
{
    public string Text;
    public DialogueNode NextNode;
}