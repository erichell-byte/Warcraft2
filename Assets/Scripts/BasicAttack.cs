using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class BasicAttack : MonoBehaviour
    {
        private float _hitLast = 0; 
        public float _hitDelay = 3f;
        
        public void Attack(Collider2D col)
        {
            GetComponent<Animator>().SetBool("isAttack", true);
            if (Time.time - _hitLast < _hitDelay)
                return;
            col.GetComponent<GameHandler>().TakeDamage(4);

            _hitLast = Time.time;
            
        }

        public void FinishAttack()
        {
            GetComponent<Animator>().SetBool("isAttack", false);
        }
    }
}
