using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotHand : MonoBehaviour
{
    [SerializeField]
    private RelativeJoint2D _grabJoint = null;

    private Animator _animator = null;
    public Action ThrowEnded;

    [SerializeField]
    public Transform _lowerHand = null;

    [SerializeField]
    public Transform _upperHand = null;


    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    public bool IsRetracted()
    {
        if (Math.Abs(_lowerHand.rotation.z) < 0.005f && Math.Abs(_upperHand.rotation.z) < 0.005f)
            return true;
        else
            return false;
    }

    public void Grab(Rigidbody2D body)
    {
        _grabJoint.enabled = true;
        _grabJoint.connectedBody = body;
    }

    public void LetGo()
    {
        _animator.enabled = false;
        _grabJoint.connectedBody = null;
        _grabJoint.enabled = false;
    }

    public void OnEndThrow()
    {
        LetGo();
        
        if (ThrowEnded != null)
            ThrowEnded();
    }

}
