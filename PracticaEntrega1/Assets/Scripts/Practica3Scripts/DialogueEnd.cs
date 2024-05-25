using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class EndNode : DialogueNode
{
    public AudioClip endAudio;
    public virtual void OnChosen(GameObject talker)
    {
        // Implementación genérica o vacía
    }
}

[CreateAssetMenu(fileName = "TheEndHappy", menuName = "Dialogue/EndNode/TheEndHappy", order = 0)]
public class TheEndHappy : EndNode
{
    public override void OnChosen(GameObject talker)
    {
        talker.GetComponent<MovementController>().Happy();
    }
}

[CreateAssetMenu(fileName = "TheEndAngry", menuName = "Dialogue/EndNode/TheEndAngry", order = 1)]
public class TheEndAngry : EndNode
{
    public override void OnChosen(GameObject talker)
    {
        talker.GetComponent<MovementController>().Angry();
    }
}

[CreateAssetMenu(fileName = "TheEndVictory", menuName = "Dialogue/EndNode/TheEndVictory", order = 2)]
public class TheEndVictory : EndNode
{
    public override void OnChosen(GameObject talker)
    {
        talker.GetComponent<MovementController>().Victory();
    }
}

[CreateAssetMenu(fileName = "TheEndDefeat", menuName = "Dialogue/EndNode/TheEndDefeat", order = 3)]
public class TheEndDefeat : EndNode
{
    public override void OnChosen(GameObject talker)
    {
        talker.GetComponent<MovementController>().Defeat();
    }
}
