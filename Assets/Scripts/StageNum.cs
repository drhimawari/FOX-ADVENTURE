using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageNum : MonoBehaviour
{
    private Text stageText = null;
    private int oldStageNum = 0;

    void Start()
    {
        stageText = GetComponent<Text>();
        if (GManager.instance != null)
        {
            stageText.text = "Stage " + GManager.instance.stageNum;
        }
        else
        {
            Debug.Log("ゲームマネージャー未設定");
            Destroy(this);
        }
    }

    void Update()
    {
        if (oldStageNum != GManager.instance.stageNum)
        {
            stageText.text = "Stage " + GManager.instance.stageNum;
            oldStageNum = GManager.instance.stageNum;
        }
    }
}
