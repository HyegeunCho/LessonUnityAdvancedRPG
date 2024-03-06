using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowCamera : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    
    void LateUpdate()
    {
        transform.position = Target.transform.position;
    }
}
