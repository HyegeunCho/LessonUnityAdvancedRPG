using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int SceneToLoad = -1;
        [SerializeField] private Transform spawnPoint;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(SceneToLoad);
            Debug.LogError($"Scene {SceneToLoad} Loaded!");

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal target)
        {
            var player = GameObject.FindWithTag("Player");
            var agent = player.GetComponent<NavMeshAgent>();
            agent.Warp(target.spawnPoint.position);
            player.transform.rotation = target.spawnPoint.rotation;

            // agent.enabled = false;
            // player.transform.position = target.spawnPoint.position;
            // player.transform.rotation = target.spawnPoint.rotation;
            // agent.enabled = true;

        } 

        private Portal GetOtherPortal()
        {
            var portals = GameObject.FindObjectsByType<Portal>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (var portal in portals)
            {
                if (portal == this) continue;
                return portal;
            }

            return null;
        }
    }
    
}
