using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class Character : MonoBehaviour
    {
        [SerializeField] bool _isSelectable;
        [SerializeField] bool _isSelected;

        public bool IsSelectable
        {
            get { return _isSelectable; }
            set { _isSelectable = value; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        public void Select(bool active)
        {
            _isSelected = active;
        }
    }
}
