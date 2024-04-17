using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int SceneToLoad = -1;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            SceneManager.LoadScene(SceneToLoad);
        }
    }
    
}
