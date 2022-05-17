using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// using Random = System.Random;

namespace MyProject
{
    
    public class EnemyMovement : MonoBehaviour
    {
        private Vector3 _destination;
        [SerializeField] private float speed = 5f;
        [SerializeField]
        private GameObject[] _humanTowers;

        [SerializeField] private BasicAttack basicAttack;
        private bool isHaveTarget = false;
        private GameObject target = null;
        private bool goBackToTown = false;
        private int randIndex;

        private void Start()
        {
            _humanTowers = new GameObject[5];
            _humanTowers[0] = GameObject.Find("HumanMainTown");
            _humanTowers[1] = GameObject.Find("HumanTown1");
            _humanTowers[2] = GameObject.Find("HumanTown2");
            _humanTowers[3] = GameObject.Find("HumanTown3");
            _humanTowers[4] = GameObject.Find("HumanTown4");
            randIndex = Random.Range(0, _humanTowers.Length);
            if (_humanTowers[randIndex] != null)
            {
                target = _humanTowers[randIndex];
            }

            GlobalEventManager.EnemyTownUnderAttack += TowerUnderAttack;

        }

        private void Update()
        {
            
            if (target == null)
            {
                randIndex = Random.Range(0, _humanTowers.Length);
                target = _humanTowers[randIndex];
            }
            if (isHaveTarget && target == null)
            {
                GetComponent<Animator>().SetBool("isAttack", false);
                isHaveTarget = false;
                
            }
            Move();
        }

        private void Move()
        {   
            if (target != null)
                _destination = target.transform.position;
            if (transform.position != _destination)
            {
                GetComponent<Animator>().SetBool("isMoving", true);
                Vector3 movement = Vector3.zero;
                Vector3 finalMov = Vector3.zero;
                float delta = speed * Time.deltaTime;
                if (transform.position.x > _destination.x)
                {
                    movement += Vector3.left;
                    finalMov.x = Mathf.Max(_destination.x, transform.position.x - delta);
                }
                else if (transform.position.x < _destination.x)
                {
                    movement += Vector3.right;
                    finalMov.x = Mathf.Min(_destination.x, transform.position.x + delta);
                }
                else
                {
                    finalMov.x = transform.position.x;
                }

                if (transform.position.y > _destination.y)
                {
                    movement += Vector3.down;
                    finalMov.y = Mathf.Max(_destination.y, transform.position.y - delta);
                }
                else if (transform.position.y < _destination.y)
                {
                    movement += Vector3.up;
                    finalMov.y = Mathf.Min(_destination.y, transform.position.y + delta);
                }
                else
                {
                    finalMov.y = transform.position.y;
                }

                transform.position = finalMov;
                //transform.position = Vector3.MoveTowards(transform.position, _destination, speed * Time.deltaTime);
                GetComponent<Animator>().SetFloat("MoveX", movement.x);
                GetComponent<Animator>().SetFloat("MoveY", movement.y);
                SoundManager.PlaySound(SoundManager.Sound.OrcMove);
            }
            else
            {
                GetComponent<Animator>().SetBool("isMoving", false);
                goBackToTown = false;
            }
        }

        private void TowerUnderAttack(GameObject newTarget)
        {
            target = newTarget;
            goBackToTown = true;
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if ((col.CompareTag("HumanTown") || col.CompareTag("Player")) && !goBackToTown)
            {
                GetComponent<Animator>().SetBool("isAttack", true);
                isHaveTarget = true;
                target = col.gameObject;
        
            }
        }
        
        private void OnTriggerStay2D(Collider2D col)
        {
            if ((col.CompareTag("HumanTown") || col.CompareTag("Player")) && !goBackToTown)
            {
                basicAttack.Attack(col);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("HumanTown") || other.CompareTag("Player"))
            {
                basicAttack.FinishAttack();
                isHaveTarget = false;
                if (!goBackToTown)
                    target = null;
            }
        }
    }
}