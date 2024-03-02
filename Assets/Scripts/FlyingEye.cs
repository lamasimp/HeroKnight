using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed= 2f;
    public List<Transform> waypoints;
    public DetectionZone biteDetectionZone;
    public bool _hasTarget = false;
    public Collider2D deathCollider;
    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;
    Transform nextWaypoint;
    int waypointNum;
    public float waypointReachedDistance = 0.1f;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }
    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(OnDeath);
    }
    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }else
            {
                rb.velocity = Vector3.zero;
            }
        }
        
    }

    private void Flight()
    {
        //flight to the next waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
       
        //check if we have the waypoint
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();
        //see if we need to switch waypoint
        if(distance <= waypointReachedDistance)
        {
            //switch to next waypoint
            waypointNum++;
            if(waypointNum >= waypoints.Count)
            {
                //loop back to original waypoint
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];
        }
        
        
    }
    void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            //facing the right
            if (rb.velocity.x < 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);

            }
        }
    }
    public void OnDeath()
    {
        //dead fall to the ground
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
