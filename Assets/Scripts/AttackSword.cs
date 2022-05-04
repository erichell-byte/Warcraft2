using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{


    public class AttackSword : MonoBehaviour, IAttack
    {
        private enum State {
            Normal,
            Attacking
        }
        
        private State state;
        private void Awake() 
        {
            SetStateNormal();
        }

        private void SetStateAttacking() {
            state = State.Attacking;
            // GetComponent<Movement>().Disable();
        }
        
        private void SetStateNormal() {
            state = State.Normal;
            // GetComponent<Movement>().Enable();
        }
        
        public void Attack(Vector3 attackDir) {
            // Attack
            SetStateAttacking();
            
            //Vector3 attackDir = (UtilsClass.GetMouseWorldPosition() - GetPosition()).normalized;
            // characterBase.PlayAttackAnimation(attackDir, SetStateNormal);
        }

        private void Update()
        {
            
        }
    }
}
