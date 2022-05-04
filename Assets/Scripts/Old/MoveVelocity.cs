using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class MoveVelocity : MonoBehaviour, IMoveVelocity
    {
        [SerializeField] private float _moveSpeed;
        private Vector3 velocityVector;
        private Rigidbody2D _rigidbody2D;

        public void SetVelocity(Vector3 velocityVector)
        {
            this.velocityVector = velocityVector;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = velocityVector * _moveSpeed;
        }
        
        public void Disable() {
            this.enabled = false;
            _rigidbody2D.velocity = Vector3.zero;
        }

        public void Enable() {
            this.enabled = true;
        }
    }
}
