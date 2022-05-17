using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;


namespace MyProject
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Vector3 _destination;
        private Transform selectedGameObject;
        [SerializeField]
        private BasicAttack basicAttack;


        public GameObject target;
        private List<TownRTS> selectedTownList;
        private List<OrcRTS> selectedOrcList;

        void Awake()
        {
            selectedTownList = new List<TownRTS>();
            selectedOrcList = new List<OrcRTS>();
            _destination = transform.position;
            selectedGameObject = gameObject.transform.Find("Selected");
        }

        void Update()
        {
            CheckNewDestiantion();
            CheckEnemyDestination();
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
                if (target != null)
                {
                    _destination = target.transform.position;
                }
                foreach (TownRTS townRTS in selectedTownList)
                {
                    if (townRTS != null)
                        townRTS.SetSelectedVisible(false);
                }
                foreach (OrcRTS orcRTS in selectedOrcList)
                {   
                    if (orcRTS != null)
                        orcRTS.SetSelectedVisible(false);
                }
                selectedTownList.Clear();
                selectedOrcList.Clear();
                Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_destination, _destination);
                foreach (Collider2D collider2D in collider2DArray)
                {
                    OrcRTS orcRTS = collider2D.GetComponent<OrcRTS>();  
                    TownRTS townRTS = collider2D.GetComponent<TownRTS>();
                    if (townRTS != null)
                    {
                        townRTS.SetSelectedVisible(true);
                        selectedTownList.Add(townRTS);

                    }
                    else if (orcRTS != null)
                    {
                        orcRTS.SetSelectedVisible(true);
                        selectedOrcList.Add(orcRTS);
                    }
                }
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
            if ((selectedTownList.Contains(col.gameObject.GetComponent<TownRTS>()) ||
                 selectedOrcList.Contains(col.gameObject.GetComponent<OrcRTS>())))
            {
                GetComponent<Animator>().SetBool("isAttack", true);
                enabled = false;
                enabled = true;
            }
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if ((selectedTownList.Contains(col.gameObject.GetComponent<TownRTS>()) || 
                selectedOrcList.Contains(col.gameObject.GetComponent<OrcRTS>())))
            {
                basicAttack.Attack(col);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            basicAttack.FinishAttack();
        }

        private void CheckEnemyDestination()
        {
            foreach (OrcRTS orcRTS in selectedOrcList)
            {
                if (orcRTS != null)
                    _destination = orcRTS.transform.position;
            }
        }
    }
}