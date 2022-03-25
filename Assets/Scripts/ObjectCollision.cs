using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [Header("踏んだ時のプレイヤーが跳ねる高さ")] public float boundHeight;

    /// <summary>
    /// プレイヤーが踏んだかどうか
    /// </summary>
    [HideInInspector] public bool playerStepOn;

    /// <summary>
    /// プレイヤーの攻撃かどうか
    /// </summary>
    [HideInInspector] public bool playerAttackOn;
}
