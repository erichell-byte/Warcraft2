using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventManager : MonoBehaviour
{
    public static Action<GameObject> EnemyTownUnderAttack;

    public static void SendTownUnderAttack(GameObject town)
    {
        if (EnemyTownUnderAttack != null) EnemyTownUnderAttack.Invoke(town);
    }
}
