using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Experimental.U2D.IK;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CrateFinder))]
public class Robot : MonoBehaviour
{
    private Rigidbody2D _robotBody = null;
    private CrateFinder _crateFinder = null;

    private float _moveSpeed = 1f;
    private float _pickupDistance = 1.5f;

    private float _throwDistance = 3.5f;


    // time counter from start of idle state
    private float _timeFromIdleStart = 0f;

    // time counter from start of attaching state
    private float _timeFromAttachStart = 0f;

    private float _waitToStart = 2f;

    // time between ik start and joint attachment
    private float _waitToAttach = 1f;


    private RobotState _robotState = RobotState.Idle;
    private Crate _targetCrate = null;

    [SerializeField]
    CCDSolver2D _ikSolver = null;

    [SerializeField]
    Animator _animator = null;

    [SerializeField]
    RobotHand _hand = null;


    void Start()
    {
        _timeFromIdleStart = 0;

        _robotBody = GetComponent<Rigidbody2D>();
        _crateFinder = GetComponent<CrateFinder>();

        _hand.ThrowEnded += OnThrowEnded;

        SetState(RobotState.Idle);
    }

    private void OnThrowEnded()
    {
        Debug.Log("Throw ending");

        AnimatorEnabled = false;
        IKEnabled = true;

        _ikSolver.GetChain(0).RestoreDefaultPose(false);

        _animator.SetBool("throwingLeft", false);
        _animator.SetBool("throwingRight", false);

        SetState(RobotState.Idle);
    }    

    private void SetState(RobotState newState)
    {
        _robotState = newState;

        Debug.Log("State " + newState.ToString());

        switch (newState)
        {
            case RobotState.Idle:

                _timeFromIdleStart = 0f;               
                _targetCrate = null;
                IKEnabled = false;
                AnimatorEnabled = false;

                break;

            case RobotState.Grabbing:

                AnimatorEnabled = false;
                IKEnabled = true;

                break;

            case RobotState.Attaching:

                break;

            case RobotState.Throwing:

                IKEnabled = false;
                AnimatorEnabled = true;                

                break;


            case RobotState.Finished:         
                AnimatorEnabled = true;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (_robotState)
        {
            case RobotState.Idle:
                ProcessIdle();
                break;

            case RobotState.DrivingToCrate:
                ProcessDrivingToCrate();
                break;

            case RobotState.Grabbing:
                ProcessGrabbing();
                break;

            case RobotState.Attaching:
                ProcessAttaching();
                break;

            case RobotState.Carrying:
                ProcessCarrying();
                break;

            case RobotState.Throwing:
                ProcessThrowing();
                break;
        }
    }

    private bool IKEnabled
    {
        get { return _ikSolver.gameObject.activeSelf; }
        set {

            if (!value) { _ikSolver.GetChain(0).target = null; }
            _ikSolver.gameObject.SetActive(value);
        }
    }

    private bool AnimatorEnabled
    {
        get { return _animator.enabled; }
        set { _animator.enabled = value; }
    }
    

    private void ProcessIdle()
    {
        _timeFromIdleStart += Time.deltaTime;

        if (_timeFromIdleStart > _waitToStart)
        {
            if (_crateFinder.CratesLeft > 0)
            {
                _targetCrate = _crateFinder.NearestCrate;
                SetState(RobotState.DrivingToCrate);
            }
            else
            {
                SetState(RobotState.Finished);
            }
        }
    }

    private void ProcessDrivingToCrate()
    {        
        float distanceToTarget = Vector2.Distance(transform.position, _targetCrate.transform.position);

        // arrived?
        if (distanceToTarget < _pickupDistance)
        {
            SetState(RobotState.Grabbing);
        }
        else
        {
            // keep moving
            if (_targetCrate.transform.position.x < transform.position.x)
            {
                _robotBody.velocity = new Vector2(-_moveSpeed, 0);
            }
            else
            {
                _robotBody.velocity = new Vector2(_moveSpeed, 0);
            }
        }
    }

    private void ProcessGrabbing()
    {
        // set ik target
        _ikSolver.GetChain(0).target = _targetCrate.transform;

        SetState(RobotState.Attaching);
    }

    private void ProcessAttaching()
    {
        _timeFromAttachStart += Time.deltaTime;

        if (_timeFromAttachStart > _waitToAttach)
        {
            // attach crate
            _hand.Grab(_targetCrate.Body);

            // set ik to default
            _ikSolver.GetChain(0).target = null;

            // need to move away?

            if (transform.position.x < -3 || transform.position.x > 3)
            {
                SetState(RobotState.Carrying);
            }
            else
            {
                SetState(RobotState.Throwing);
            }            
        }
    }

    private void ProcessCarrying()
    {
        bool redCrateTooClose = transform.position.x < -3;
        bool blueCrateTooClose = transform.position.x > 3;

        if (redCrateTooClose)
        {
            _robotBody.velocity = new Vector2(_moveSpeed, 0);

        }

        else if (blueCrateTooClose)
        {
            _robotBody.velocity = new Vector2(-_moveSpeed, 0);

        }
        else
        {
            SetState(RobotState.Throwing);
        }      
    }

    private void ProcessThrowing()
    {      

        AnimatorEnabled = true;

        if (_targetCrate.Color == CrateColor.Red)
        {
            // throw left
            _animator.SetBool("throwingLeft", true);
            _animator.SetBool("throwingRight", false);
        }
        else
        {
            // throw right
            _animator.SetBool("throwingLeft", false);
            _animator.SetBool("throwingRight", true);
        }        
    }   
}
