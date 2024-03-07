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
        // Start is called before the first frame update
    
        void Start()
        {
            _mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            CombatTarget target = hits.Select(hit => hit.transform.GetComponent<CombatTarget>())
                .FirstOrDefault(t => t != null);

            if (target == null) return;
            
            if (Input.GetMouseButton(0))
            {
                GetComponent<Fighter>().Attack(target);
            }
        }
        
        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                if (!Physics.Raycast(GetMouseRay(), out RaycastHit hit)) return;
            
                _mover.MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}