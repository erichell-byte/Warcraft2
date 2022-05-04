using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace MyProject
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Vector3 _destination;
        private Transform selectedGameObject;
        public AttackSword AttackSword;
        private float _hitLast = 0;
        public float _hitDelay = 3f;

        void Awake()
        {
            _destination = transform.position;
            selectedGameObject = gameObject.transform.Find("Selected");
        }

        void Update()
        {
            CheckNewDestiantion();
            Move();
        }

        private void CheckNewDestiantion()
        {
            if (Input.GetMouseButtonDown(1) && selectedGameObject.gameObject.activeSelf)
            {
                Vector3 mousePosition = Input.mousePosition;
                _destination = Camera.main.ScreenToWorldPoint(mousePosition);
                _destination.z = mousePosition.z;
                _destination = WorldpointToSquaredPoint(_destination);
            }
        }

        private Vector3 WorldpointToSquaredPoint(Vector3 destination)
        {
            float x = SquareCoordinate(destination.x);
            float y = SquareCoordinate(destination.y);
            return new Vector3(x, y, destination.z);
        }

        private float SquareCoordinate(float coordinate)
        {
            int cInt = Mathf.FloorToInt(Mathf.Abs(coordinate));
            float cFloat = Mathf.Abs(coordinate % cInt);
            if (cInt == 0) cFloat = Mathf.Abs(_destination.x);
            float sign = Mathf.Sign(coordinate);
            if (cFloat < 0.5f && (0.5f - cFloat) < coordinate - cInt) coordinate = sign * (cInt + 0.5f);
            else if (cFloat < 0.5f && (0.5f - cFloat) > coordinate - cInt) coordinate = sign * cInt;
            else if (cFloat > 0.5f && (cFloat - 0.5f) < cInt + 1 - coordinate) coordinate = sign * (cInt + 1);
            else if (cFloat > 0.5f && (cFloat - 0.5f) > cInt + 1 - coordinate) coordinate = sign * (cInt + 0.5f);
            return coordinate;
        }

        private void Move()
        {
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
                SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
            }
            else
            {
                GetComponent<Animator>().SetBool("isMoving", false);
            }

        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("OrcTown"))
            {
                GetComponent<Animator>().SetBool("isAttack", true);
                
            }
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("OrcTown"))
            {
                if (Time.time - _hitLast < _hitDelay)
                    return;
                
                GetComponent<Animator>().SetBool("isAttack", true);
                col.GetComponent<GameHandler>().TakeDamage(4);

                _hitLast = Time.time;
            }
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("OrcTown"))
            {
                GetComponent<Animator>().SetBool("isAttack", false);
            }
        }
    }
}