using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor;

namespace MyProject
{
    public class GameRTSController : MonoBehaviour
    {

        [SerializeField] private Transform _selectionAreaTransform;
        private Vector3 startPosition;
        private List<UnitRTS> selectedUnitRTSList;

        private void Awake()
        {
            selectedUnitRTSList = new List<UnitRTS>();
            _selectionAreaTransform.gameObject.SetActive(false);
            SoundManager.Initialize();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _selectionAreaTransform.gameObject.SetActive(true);
                startPosition = UtilsClass.GetMouseWorldPosition();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
                Vector3 lowerLeft = new Vector3(Mathf.Min(startPosition.x, currentMousePosition.x),
                                                Mathf.Min(startPosition.y, currentMousePosition.y));
                Vector3 upperRight = new Vector3(Mathf.Max(startPosition.x, currentMousePosition.x),
                                                Mathf.Max(startPosition.y, currentMousePosition.y));

                _selectionAreaTransform.position = lowerLeft;
                _selectionAreaTransform.localScale = upperRight - lowerLeft;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _selectionAreaTransform.gameObject.SetActive(false);
                Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());
                foreach (UnitRTS unitRts in selectedUnitRTSList)
                {
                    if (unitRts != null)
                    unitRts.SetSelectedVisible(false);
                }
                selectedUnitRTSList.Clear();
                foreach (Collider2D collider2D in collider2DArray)
                {
                    UnitRTS unitRTS = collider2D.GetComponent<UnitRTS>();
                    if (unitRTS != null)
                    {
                        unitRTS.SetSelectedVisible(true);
                        selectedUnitRTSList.Add(unitRTS);
                        SoundManager.PlaySound(SoundManager.Sound.PlayerSelected);
                    }
                }
                // Debug.Log(selectedUnitRTSList.Count);
                
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
