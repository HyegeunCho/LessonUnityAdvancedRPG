using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private void Update()
        {
            if (DistanceToPlayer() <= chaseDistance)
            {
                Debug.LogError($"[{gameObject.name}] Start Chase");
            }
        }

        private float DistanceToPlayer()
        {
            var player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }    
}

