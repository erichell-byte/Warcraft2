using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] ActiveSelection _selectionPrefab;

        Vector3 _startSelectionPoint;
        Vector3 _currentSelectionPoint;
        private ActiveSelection _currentSelection;


        private void Awake()
        {
            _currentSelection = Instantiate(_selectionPrefab, transform);
        }
        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                _startSelectionPoint = Camera.main.ScreenToWorldPoint(mousePosition);
                _currentSelectionPoint = _startSelectionPoint;

                _currentSelection.StartSelection(_startSelectionPoint, _currentSelectionPoint);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Input.mousePosition;
                _currentSelectionPoint = Camera.main.ScreenToWorldPoint(mousePosition);
                _currentSelection.MakeSelection(_startSelectionPoint, _currentSelectionPoint);
            }
            if (Input.GetMouseButtonUp(0))
            {
                _currentSelection.Select();
            }
        }
    }
}
