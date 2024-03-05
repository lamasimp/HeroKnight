using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;  
    Animator animator;
    [SerializeField] private int _maxHealth = 100;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;
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
            healthChanged?.Invoke(_health, MaxHealth);
            if(_health <= 0 )
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
            if(value == false)
            {
                damageableDeath.Invoke();
            }
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

    }
    //check the damageable took damage or not
    public bool Hit(int damage, Vector2 knockBack)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockBack);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        //unable to be hit
        return false;
    }
    //return whether the char was heal or not
    public bool Heal(int healthRestored)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health,0);
            int actualHeal = Mathf.Min(maxHeal, healthRestored);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
