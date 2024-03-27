using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Mover))]
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Fighter _fighter;
        private Health _health;
    
        void Start()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_health?.IsDead() ?? true) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            var target = hits.Select(hit => hit.transform.gameObject).FirstOrDefault(go =>
            {
                var combatTarget = go.transform.GetComponent<CombatTarget>();
                var health = go.transform.GetComponent<Health>();
                if (combatTarget == null || health == null) return false;
                return !health.IsDead();
            });
            
            if (target == null) return false;
            
            if (Input.GetMouseButton(0))
            {
                _fighter.Attack(target);
            }
            return true;
        }
        
        private bool InteractWithMovement()
        {
            if (!Physics.Raycast(GetMouseRay(), out RaycastHit hit)) return false;
            if (Input.GetMouseButton(0))
            {
                _mover.StartMoveAction(hit.point, 1f);
            }
            return true;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}