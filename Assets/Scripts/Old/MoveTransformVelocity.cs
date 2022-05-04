using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{

    public class MoveTransformVelocity : MonoBehaviour, IMoveVelocity
    {
        [SerializeField] private float _moveSpeed;
        private Vector3 velocityVector;


        public void SetVelocity(Vector3 velocityVector)
        {
            this.velocityVector = velocityVector;
        }

        private void Update()
        {
            transform.position += velocityVector * _moveSpeed * Time.deltaTime;
            
        }
        
        public void Disable() {
            this.enabled = false;
        }

        public void Enable() {
            this.enabled = true;
        }
    }
}

