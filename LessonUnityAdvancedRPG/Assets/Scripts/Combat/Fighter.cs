using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float weaponRange = 2f;
        
        private Transform target;

        private void Update()
        {
            if (target != null && Vector3.Distance(target.position, transform.position) > weaponRange)
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        public void Attack(CombatTarget inTarget)
        {
            target = inTarget.transform;
        }
    }
}