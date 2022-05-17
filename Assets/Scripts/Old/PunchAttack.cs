using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

namespace MyProject
{
    public class PunchAttack : MonoBehaviour
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
            GetComponent<IMoveVelocity>().Disable();
        }
        
        private void SetStateNormal() {
            state = State.Normal;
            GetComponent<IMoveVelocity>().Enable();
        }
        
        public void Attack() {
            // Attack
            SetStateAttacking();
            
            //Vector3 attackDir = (UtilsClass.GetMouseWorldPosition() - GetPosition()).normalized;
            // characterBase.PlayAttackAnimation(attackDir, SetStateNormal);
        }
        private Vector3 GetPosition() {
            return transform.position;
        }
    }
}
