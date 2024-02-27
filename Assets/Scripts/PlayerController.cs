using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    public float jumpImpluse = 8f;

    public float moveSpeed = 5f;
    public float airWalkSpeed = 2f;

    public float runSpeed = 8f;
    Vector2 moveInput;

    public float CurrentMoveSpeed { get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGround)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return moveSpeed;
                        }
                    }
                    else
                    {
                        return airWalkSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            
        } }

    [SerializeField]
    private bool _isMoving = false;
    TouchingDirections touchingDirections;
    public bool IsMoving { get
        {
            return _isMoving;
        } private set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.isMoving, value);
        } }
    [SerializeField]
    private bool _isRunning = false;
        public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationString.isRunning, value);
        }
    }

    public bool _isFacingRight = true;
    
    public bool IsFacingRight { get { return _isFacingRight; } private set { 
        if( _isFacingRight != value) {
                transform.localScale *= new Vector2(-1, 1);
            }
        _isFacingRight = value;
        } }

    public Animator animator;
    public float high = 8f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }


    
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed ,rb.velocity.y);
        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context) 
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGround && CanMove)
        {
            animator.SetTrigger(AnimationString.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpluse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.attackTrigger);
        }
    }
    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            //face the right
            IsFacingRight = true;
        }else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public bool CanMove { get
        {
            return animator.GetBool(AnimationString.canMove);
        } }

}
