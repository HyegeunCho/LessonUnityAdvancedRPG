using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    [RequireComponent(typeof(Mover))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private Mover _mover;
        private Fighter _fighter;
        private GameObject _player;
        private Health _health;
        
        private void Start()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (_health?.IsDead() ?? true) return;
            if (InteractWithCombat()) return;
            // if (InteractWithMovement()) return;

            if (!InAttackRangeOfPlayer())
            {
                // _mover.Cancel();
                _fighter.Cancel();
                return;
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(_player.transform.position, transform.position) <= chaseDistance;
        }

        private bool InteractWithCombat()
        {
            if (!InAttackRangeOfPlayer()) return false;
            
            var health = _player?.GetComponent<Health>();
            if (health == null || health.IsDead()) return false;
            
            _fighter.Attack(_player);
            return true;
        }

        private bool InteractWithMovement()
        {
            return false;

            // var player = GameObject.FindWithTag("Player");
            // if (player == null) return false;
            //
            // if (DistanceToPlayer() <= chaseDistance)
            // {
            //     _mover.StartMoveAction(player.transform.position);
            // }
            //
            // return true;
        }
    }    
}

