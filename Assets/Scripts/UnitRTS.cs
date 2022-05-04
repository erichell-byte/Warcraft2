using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class UnitRTS : MonoBehaviour
    {
        private GameObject selectedGameObject;

        private void Awake()
        {
            selectedGameObject = transform.Find("Selected").gameObject;
            SetSelectedVisible(false);
        }

        public void SetSelectedVisible(bool visible)
        {
            selectedGameObject.SetActive(visible);
        }
    }
}
