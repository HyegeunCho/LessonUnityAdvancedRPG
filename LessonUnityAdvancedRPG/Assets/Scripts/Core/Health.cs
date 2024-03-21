using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        public float HealthValue => health;

        private bool isDead = false;
        public bool IsDead() => isDead;
        
        private Animator _animator;
        private ActionScheduler _actionScheduler;

        private void Start()
        {
            _animator ??= GetComponent<Animator>();
            _actionScheduler ??= GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            float prevHealth = health;
            health = Math.Max(0, health - damage);
            Debug.LogError($"[Enemy] Health: {health}");

            if (health == 0)
            {
                Die();
            }
            
            
            // if (Math.Abs(prevHealth - health) < 0.01f) return;
            // if (health <= 0) _animator.SetTrigger("die");
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            _animator.SetTrigger("die");
            _actionScheduler?.CancelCurrentAction();
        }
    }
}
    
