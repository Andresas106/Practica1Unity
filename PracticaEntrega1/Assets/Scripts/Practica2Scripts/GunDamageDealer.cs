using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamageDealer : MonoBehaviour
{
        public float Enemydamage = 10, Playerdamage = 20;

        public void CauseDamage(GameObject target, string attackerTag)
        {
            var go = target.GetComponent<ITakeDamage>();

            if (go != null)
            {
                if (attackerTag == "player")
                {
                    go.TakeDamage(Playerdamage, true);
                }
            }
        }
}
