using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        public bool IsTriggered { private set; get; } = false;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (IsTriggered) return;

            IsTriggered = true;
            GetComponent<PlayableDirector>()?.Play();
        }
    }
}