using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveParticles : MonoBehaviour
{
    bool isWalking;
    bool isRunning;
    bool isJumping;

    public Animator animatorPlayer;

    void Update()
    {
        isWalking = animatorPlayer.GetBool("isWalking");
        isRunning = animatorPlayer.GetBool("isRunning");
        isJumping = animatorPlayer.GetBool("isJumping");

        if(isWalking && !isRunning && !isJumping)
        {
            
        }
        else if(isWalking && isRunning && !isJumping)
        {
            
        }
        else if(isJumping)
        {
            
        }

            

    }


}
