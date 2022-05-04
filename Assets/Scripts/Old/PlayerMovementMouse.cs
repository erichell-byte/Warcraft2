using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

namespace MyProject
{
    public class PlayerMovementMouse : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<IMovePosition>().SetMovePosition(UtilsClass.GetMouseWorldPosition());
            }
        }
    }
}
