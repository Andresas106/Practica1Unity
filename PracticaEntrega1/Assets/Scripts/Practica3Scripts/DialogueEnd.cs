using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
//EndNode hereda de DialogNode
public class EndNode : DialogueNode
{
    //Con esta variable pública se asigna el audio que ha de sonar cuando termina el dialogo
    public AudioClip endAudio;
    public virtual void OnChosen(GameObject talker)
    {
        // Implementación genérica o vacía
    }
}
//Final Feliz
[CreateAssetMenu(fileName = "TheEndHappy", menuName = "Dialogue/EndNode/TheEndHappy", order = 0)]
public class TheEndHappy : EndNode
{
    //Sobreescribe el método Onchosen de la clase base EndNode.
    public override void OnChosen(GameObject talker)
    {
        // Llama al método Happy del componente MovementController del objeto talker.
        talker.GetComponent<MovementController>().Happy();
    }
}
//Final enfado
[CreateAssetMenu(fileName = "TheEndAngry", menuName = "Dialogue/EndNode/TheEndAngry", order = 1)]
public class TheEndAngry : EndNode
{
    public override void OnChosen(GameObject talker)
    {
        talker.GetComponent<MovementController>().Angry();
    }
}
//Final Victoria
[CreateAssetMenu(fileName = "TheEndVictory", menuName = "Dialogue/EndNode/TheEndVictory", order = 2)]
public class TheEndVictory : EndNode
{
    public override void OnChosen(GameObject talker)
    {
        talker.GetComponent<MovementController>().Victory();
    }
}
//Final derrota
[CreateAssetMenu(fileName = "TheEndDefeat", menuName = "Dialogue/EndNode/TheEndDefeat", order = 3)]
public class TheEndDefeat : EndNode
{
    public override void OnChosen(GameObject talker)
    {
        talker.GetComponent<MovementController>().Defeat();
    }
}
