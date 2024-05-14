using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConversation", menuName = "Dialog/Conversation")]
public class Conversation : ScriptableObject
{
    public string Name;
    public DialogueNode StarNode;
}
