using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Animator))]
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttack = 1f;
        [SerializeField] private float weaponDamage = 5f;
        
        private Health target;
        
        private Animator _animator;

        private float timeSinceLastAttack = 0;
        
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if (target == null) return;
            if (target.IsDead()) return;
            
            if (Vector3.Distance(target.transform.position, transform.position) > weaponRange)
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack < timeBetweenAttack) return;
            
            TriggerAttack();
            timeSinceLastAttack = 0;
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger("stopAttack");
            _animator.SetTrigger("attack");
        }
        
        // Animation Event 
        void Hit()
        {
            target?.TakeDamage(weaponDamage);
        }

        public void Attack(GameObject inCombatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = inCombatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject inTarget)
        {
            var health = inTarget?.GetComponent<Health>();
            if (health == null) return false;
            return !health.IsDead();
        }
    }
}