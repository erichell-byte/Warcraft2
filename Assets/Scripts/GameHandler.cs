using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            HealthBar healthBar = pfHealthBar.GetComponent<HealthBar>();
            healthBar.Setup(_healthSystem);
        }


        public void TakeDamage(int damage)
        {
            _healthSystem.Damage(damage);
            Debug.Log(gameObject.name + " health is "+ _healthSystem.GetHealth());
            if (gameObject.CompareTag("OrcTown"))
            {
                GlobalEventManager.SendTownUnderAttack(gameObject);
            }
            if (_healthSystem.GetHealth() <= 0)
            {
                if (gameObject.name == "HumanMainTown")
                {
                    Debug.Log("ORCS WIN THIS BATTLE");
                    Time.timeScale = 0;
                }
                else if (gameObject.name == "OrcMainTown")
                {
                    Debug.Log("HUMANS WIN THIS BATTLE");
                    Time.timeScale = 0;
                }
                Destroy(gameObject);
            }
        }

        public int GetHealth()
        {
            return (_healthSystem.GetHealth());
        }
    }
}


    
