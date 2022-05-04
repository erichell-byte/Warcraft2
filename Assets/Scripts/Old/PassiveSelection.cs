using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class PassiveSelection : MonoBehaviour
    {
        [SerializeField] GameObject _topSelectionEdge;
        [SerializeField] GameObject _botSelectionEdge;
        [SerializeField] GameObject _leftSelectionEdge;
        [SerializeField] GameObject _rightSelectionEdge;
        [SerializeField] float _defaultSelectionSize = 0.02f;

        protected float width, height;

        public void Draw(Transform selectableUnit)
        {
            Vector3 firstPoint = new Vector3(selectableUnit.position.x - selectableUnit.localScale.x / 2,
                selectableUnit.position.y - selectableUnit.localScale.y / 2, 0);
            Vector3 secondPoint = new Vector3(selectableUnit.position.x + selectableUnit.localScale.x / 2,
                selectableUnit.position.y + selectableUnit.localScale.y / 2, 0);
            Draw(firstPoint, secondPoint);
        }

        public void Draw(Vector3 firstPoint, Vector3 secondPoint)
        {
            width = Mathf.Abs(firstPoint.x - secondPoint.x);
            height = Mathf.Abs(firstPoint.y - secondPoint.y);
            float xLeft, yBot, xRight, yTop;
            if (firstPoint.x > secondPoint.x)
            {
                xLeft = secondPoint.x;
                xRight = secondPoint.x + width;
            }
            else
            {
                xLeft = firstPoint.x;
                xRight = firstPoint.x + width;

            }

            if (firstPoint.y > secondPoint.y)
            {
                yBot = secondPoint.y;
                yTop = secondPoint.y + height;
            }
            else
            {
                yBot = firstPoint.y;
                yTop = firstPoint.y + height;
            }

            _topSelectionEdge.transform.position = new Vector3(xLeft + width / 2, yTop, 0);
            _botSelectionEdge.transform.position = new Vector3(xLeft + width / 2, yBot, 0);
            _leftSelectionEdge.transform.position = new Vector3(xLeft, yBot + height / 2, 0);
            _rightSelectionEdge.transform.position = new Vector3(xRight, yBot + height / 2, 0);

            _topSelectionEdge.transform.localScale =
                new Vector3(width + _defaultSelectionSize, _defaultSelectionSize, 1);
            _botSelectionEdge.transform.localScale =
                new Vector3(width + _defaultSelectionSize, _defaultSelectionSize, 1);
            _leftSelectionEdge.transform.localScale = new Vector3(_defaultSelectionSize, height, 1);
            _rightSelectionEdge.transform.localScale = new Vector3(_defaultSelectionSize, height, 1);
        }

    }
}
