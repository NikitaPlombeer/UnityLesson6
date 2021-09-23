using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RotateToPlayer : MonoBehaviour
    {

        public Vector3 LeftEuler;
        public Vector3 RightEuler;
        public float RotationSpeed = 5f;
        
        private Vector3 targetEuler;
        private Transform _playerTransform;

        private void Start()
        {
            _playerTransform = FindObjectOfType<PlayerController>().transform;
        }

        private void Update()
        {
            if (transform.position.x < _playerTransform.transform.position.x)
            {
                targetEuler = RightEuler;
            }
            else
            {
                targetEuler = LeftEuler;
            }

            transform.rotation = Quaternion.Lerp(
                transform.rotation, 
                Quaternion.Euler(targetEuler),
                Time.deltaTime * RotationSpeed
            );
        }
        
        
    }
}