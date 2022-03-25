using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText = null;
    private int oldScore = 0;

    void Start()
    {
        scoreText = GetComponent<Text>();
        if (GManager.instance != null)
        {
            scoreText.text = "Score " + GManager.instance.score;
        }
        else
        {
            Debug.Log("ゲームマネージャー未設定");
            Destroy(this);
        }
    }

    void Update()
    {
        if (oldScore != GManager.instance.score)
        {
            scoreText.text = "Score " + GManager.instance.score;
            oldScore = GManager.instance.score;
        }
    }
}
