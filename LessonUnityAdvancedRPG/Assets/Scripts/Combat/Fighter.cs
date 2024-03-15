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
        
        private Transform target;
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
            
            if (Vector3.Distance(target.position, transform.position) > weaponRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack < timeBetweenAttack) return;
            
            _animator.SetTrigger("attack");
            timeSinceLastAttack = 0;
        }
        
        // Animation Event 
        void Hit()
        {
            var health = target?.GetComponent<Health>();
            health?.TakeDamage(weaponDamage);
        }

        public void Attack(CombatTarget inTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = inTarget.transform;
        }

        public void StartAction(Transform inTransform)
        {
            target = inTransform;
        }

        public void Cancel()
        {
            target = null;
            _animator.ResetTrigger("attack");
        }
    }
}