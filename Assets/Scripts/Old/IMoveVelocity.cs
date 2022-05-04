using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public interface IMoveVelocity
    {
        void SetVelocity(Vector3 velocityVector);

        void Enable();

        void Disable();
    }
}
