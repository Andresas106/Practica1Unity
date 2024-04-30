using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperBodyAnimation : MonoBehaviour
{
    Animator _animator;
    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        float w = Mathf.Sin(Time.time) / 2 + 0.5f;
        //_animator.SetLayerWeight(1, w);

    }
}
