using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;
    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField] private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if(_health < 0 )
            {
                IsAlive = false;
            }
        }
    }
    private bool _isAlive = true;
    [SerializeField] private bool isInvincible = false;
    private float timeSinceHit;
    public float invincibilityTimer = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive, value);
            Debug.Log("IsAlive set " + value);
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTimer)
            {
                isInvincible=false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        Hit(10);
    }
    public void Hit(int damage)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
        }
    }
    
}
