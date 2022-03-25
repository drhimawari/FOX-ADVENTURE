using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [Header("���Z����X�R�A")] public int myScore;
    [Header("�v���C���[�̔���")] public PlayerTriggerCheck playerCheck;
    [Header("�A�C�e���擾���ɖ炷SE")] public AudioClip itemSE;

    void Update()
    {
        //�v���C���[��������ɓ�������
        if (playerCheck.isOn)
        {
            if (GManager.instance != null)
            {
                //�X�R�A���Z
                GManager.instance.score += myScore;
                //SE
                GManager.instance.PlaySE(itemSE);
                //�j��
                Destroy(this.gameObject);
            }
        }
    }
}
