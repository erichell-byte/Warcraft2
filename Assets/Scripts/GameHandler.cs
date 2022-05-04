using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class GameHandler : MonoBehaviour
    {
        public Transform pfHealthBar;
        public int healhMax = 100;
        private HealthSystem _healthSystem;
        private void Start()
        {
            _healthSystem = new HealthSystem(healhMax);
            // Transform healthBarTransform = transform.Find("phHealthBar");
            // Instantiate(pfHealthBar, new Vector3(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            HealthBar healthBar = pfHealthBar.GetComponent<HealthBar>();
            healthBar.Setup(_healthSystem);
            // Debug.Log("Health:" + _healthSystem.GetHealthPercent());
            // _healthSystem.Damage(100);
            
            // Debug.Log("Health:" + _healthSystem.GetHealthPercent());
        }

        private void Update()
        {
            // if (_healthSystem.GetHealth() == 0)
            //     Destroy(gameObject);
        }


        public void TakeDamage(int damage)
        {
            _healthSystem.Damage(damage);
            if (_healthSystem.GetHealth() == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}


    
