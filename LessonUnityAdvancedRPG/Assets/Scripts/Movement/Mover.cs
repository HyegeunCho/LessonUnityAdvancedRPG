using System;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;

        void Start()
        {
            _agent = transform.GetComponent<NavMeshAgent>();
            _animator = transform.GetComponent<Animator>();
        }

        void Update()
        {
            // if (Input.GetMouseButton(0))
            // {
            //     MoveToCursor();
            // }
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }
    
        private void UpdateAnimator()
        {
            /// _agent.velocity는 월드에서의 유닛의 위치와 목적지에 대한 속도를 갖고 있음
            /// 애니메이터는 플레이어 자체 (로컬)에 속해 있고 플레이어 자체의 전진 속도만 필요하기 때문에
            /// 로컬로 변환해서 사용하는 것. 
        
            // Global을 Local로 변환 처리
            var localVelocity = transform.InverseTransformDirection(_agent.velocity);
            _animator.SetFloat("forwardSpeed", localVelocity.z);
        }
    }
}