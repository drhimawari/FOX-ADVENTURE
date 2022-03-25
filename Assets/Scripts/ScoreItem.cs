using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [Header("加算するスコア")] public int myScore;
    [Header("プレイヤーの判定")] public PlayerTriggerCheck playerCheck;
    [Header("アイテム取得時に鳴らすSE")] public AudioClip itemSE;

    void Update()
    {
        //プレイヤーが判定内に入ったら
        if (playerCheck.isOn)
        {
            if (GManager.instance != null)
            {
                //スコア加算
                GManager.instance.score += myScore;
                //SE
                GManager.instance.PlaySE(itemSE);
                //破棄
                Destroy(this.gameObject);
            }
        }
    }
}
