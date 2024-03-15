using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Math.Max(0, health - damage);
            Debug.LogError($"[Enemy] Health: {health}");
        }
    }
}
    
