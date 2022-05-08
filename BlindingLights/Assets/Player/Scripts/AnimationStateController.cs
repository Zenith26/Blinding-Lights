using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //increases performance
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardmovement  = Input.GetKey("w");
        bool run = Input.GetKey("left shift");

        //Forward Player Movement
        if(!isWalking && forwardmovement)
        {
            animator.SetBool(isWalkingHash, true);
        }

        //player stops walking
        if(isWalking && !forwardmovement)
        {
            animator.SetBool(isWalkingHash, false);
        }

        //Player is walking and not running and presses left shift
        if(!isRunning && (forwardmovement && run))
        {
            animator.SetBool(isRunningHash, true);
        }

        //player stops running and stops running or stops walking
        if(isRunning && (!forwardmovement || !run))
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
