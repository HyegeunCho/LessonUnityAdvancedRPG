using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Mover))]
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;

        private Fighter _fighter;
        // Start is called before the first frame update
    
        void Start()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            CombatTarget target = hits.Select(hit => hit.transform.GetComponent<CombatTarget>())
                .FirstOrDefault(t => t != null);
            if (target == null) return false;
            
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Fighter>().Attack(target);
            }
            return true;
        }
        
        private bool InteractWithMovement()
        {
            if (!Physics.Raycast(GetMouseRay(), out RaycastHit hit)) return false;
            if (Input.GetMouseButton(0))
            {
                _mover.StartMoveAction(hit.point);
                // _fighter.Cancel();
            }
            return true;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}