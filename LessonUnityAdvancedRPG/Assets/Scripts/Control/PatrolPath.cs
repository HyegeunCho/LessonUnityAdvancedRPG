using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private const float waypointGizmoRadius = 0.3f;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount; i++)
            {
                var current = GetWaypoint(i);
                Gizmos.DrawSphere(current, waypointGizmoRadius);
                var next = GetNextWaypoint(i);
                Gizmos.DrawLine(current, next);
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i % transform.childCount).position;
        }

        public Vector3 GetNextWaypoint(int i)
        {
            return GetWaypoint(GetNextIndex(i));
        }

        public int GetNextIndex(int inCurrentIndex)
        {
            return (inCurrentIndex + 1) % transform.childCount;
        }
    }
    
}
