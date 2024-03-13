using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        
        private Transform target;

        private void Update()
        {
            if (target == null) return;
            
            if (Vector3.Distance(target.position, transform.position) > weaponRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
            }
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
        }
    }
}