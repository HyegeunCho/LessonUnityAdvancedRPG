using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private GameObject Target;
    
        void LateUpdate()
        {
            transform.position = Target.transform.position;
        }
    }
}