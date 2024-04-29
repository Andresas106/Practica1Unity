using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSystem : MonoBehaviour
{

    Animator _animator;
    public Transform RightHandTarget;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);

    }
}
