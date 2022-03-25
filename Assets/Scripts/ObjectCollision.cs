using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    [Header("���񂾎��̃v���C���[�����˂鍂��")] public float boundHeight;

    /// <summary>
    /// �v���C���[�����񂾂��ǂ���
    /// </summary>
    [HideInInspector] public bool playerStepOn;

    /// <summary>
    /// �v���C���[�̍U�����ǂ���
    /// </summary>
    [HideInInspector] public bool playerAttackOn;
}
