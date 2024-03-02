using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    public float walkAcceleration = 30f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;

    public DetectionZone attackZone;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Damageable damageable;
    Animator animator;
    public DetectionZone cliffDetectionZone;
    public enum WalkableDirection { Right , Left}
    private Vector2 walkDirectionVector = Vector2.right;
    private WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
        
    }
    public bool _hasTarget = false;

    public bool HasTarget { get { return _hasTarget; } private set 
        { 
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        } }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationString.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationString.lockVelocity, value);
        }
    }

    public float AttackCoolDown { 
        get 
        {
            return animator.GetFloat(AnimationString.attackCoolDown);
        } private set
        {
            animator.SetFloat(AnimationString.attackCoolDown, Mathf.Max(value,0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;   
        if(AttackCoolDown >0)
            AttackCoolDown -= Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGround && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove)
                //acceleration towards max speed
               
                rb.velocity = new Vector2(Mathf.
                    Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);

        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        } else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal value of right or left");
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnCliffDetected()
    {
        if (touchingDirections.IsGround)
        {
            FlipDirection();
        }
    }
}
