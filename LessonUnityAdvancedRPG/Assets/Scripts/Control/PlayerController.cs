using System;
using System.Collections;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
        
        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out RaycastHit hit)) return;
            
                _mover.MoveTo(hit.point);
            }
        }
    }
}