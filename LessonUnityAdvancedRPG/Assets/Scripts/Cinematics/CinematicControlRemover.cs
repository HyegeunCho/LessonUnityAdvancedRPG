using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayableDirector director;
        private GameObject player;
        
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            
            director = GetComponent<PlayableDirector>();
            director.played += DisableControl;
            director.stopped += EnableControl;
        }

        void DisableControl(PlayableDirector inDirector)
        {
            if (player == null) return;
            player?.GetComponent<ActionScheduler>()?.CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
            
            print("DisableControl");
        }

        void EnableControl(PlayableDirector inDirector)
        {
            if (player == null) return;
            player.GetComponent<PlayerController>().enabled = true;
            
            print("EnableControl");
        }
    }    
}

