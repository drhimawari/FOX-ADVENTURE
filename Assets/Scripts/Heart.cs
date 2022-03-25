using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    private Text heartText = null;
    private int oldHeartNum = 0;

    void Start()
    {
        heartText = GetComponent<Text>();
        if (GManager.instance != null)
        {
            heartText.text = "�~ " + GManager.instance.heartNum;
        }
        else
        {
            Debug.Log("�Q�[���}�l�[�W���[���ݒ�");
            Destroy(this);
        }
    }

    void Update()
    {
        if (oldHeartNum != GManager.instance.heartNum)
        {
            heartText.text = "�~ " + GManager.instance.heartNum;
            oldHeartNum = GManager.instance.heartNum;
        }
    }
}
