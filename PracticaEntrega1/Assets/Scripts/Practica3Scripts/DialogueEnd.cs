using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoNothing", menuName = "Dialogue/EndNode/DoNothing", order = 1)]
public class EndNode : DialogueNode
{
    public virtual void OnChosen(GameObject talker)
    {
        talker.GetComponent<MovementController>().Happy();
    }
}