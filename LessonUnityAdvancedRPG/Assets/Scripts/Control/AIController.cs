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
        [SerializeField] private float suspicionSecond = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float waypointDwellingSecond = 3f;
        [Range(0, 1)] [SerializeField] private float patrolSpeedFraction = 0.2f;
        
        private Mover _mover;
        private Fighter _fighter;
        private GameObject _player;
        private Health _health;

        private Vector3 _guardPosition;
        private Vector3 _suspicionPosition;
        
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceLastAtWaypoint = Mathf.Infinity;
        
        private void Start()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _player = GameObject.FindWithTag("Player");

            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (_health?.IsDead() ?? true) return;
            if (InteractWithCombat())
            {
                AttackBehaviour();
            }
            else if (_timeSinceLastSawPlayer < suspicionSecond)
            {
                SuspicionBehaviour();
            }
            else if (!InAttackRangeOfPlayer())
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceLastAtWaypoint += Time.deltaTime;   
        }

        private void AttackBehaviour()
        {
            _timeSinceLastSawPlayer = 0;
            _fighter.Attack(_player);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = GetCurrentWaypoint();

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceLastAtWaypoint = 0;
                    nextPosition = CycleWaypoint();
                }
            }

            if (_timeSinceLastAtWaypoint > waypointDwellingSecond)
            {
                _mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private Vector3 CycleWaypoint()
        {
            _waypointIndex = patrolPath.GetNextIndex(_waypointIndex);
            return patrolPath.GetWaypoint(_waypointIndex);
        }

        private int _waypointIndex = 0;
        public Vector3 GetCurrentWaypoint()
        {
            return patrolPath?.GetWaypoint(_waypointIndex) ?? _guardPosition;
        }

        private void SetWaypoint(int i)
        {
            _waypointIndex = i;
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }    
}

