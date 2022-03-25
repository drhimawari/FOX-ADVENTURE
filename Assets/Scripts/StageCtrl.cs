using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageCtrl : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("コンティニュー位置")] public GameObject[] continuePoint;
    [Header("ゲームオーバー")] public GameObject gameOverObj;
    [Header("フェード")] public FadeImage fade;
    [Header("ゲームオーバー時に鳴らすSE")] public AudioClip gameOverSE;
    [Header("リトライ時に鳴らすSE")] public AudioClip retrySE;
    [Header("ステージクリアーSE")] public AudioClip stageClearSE;
    [Header("ステージクリア")] public GameObject stageClearObj;
    [Header("ステージクリア判定")] public PlayerTriggerCheck stageClearTrigger;
    [Header("ステージクリア後BGM")] public AudioClip stageClearBGM;
    #endregion

    #region//プライベート変数
    private Player p;
    private int nextStageNum;
    private bool startFade = false;
    private bool doGameOver = false;
    private bool retryGame = false;
    private bool doSceneChange = false;
    private bool doClear = false;
    private int finalStage = 3;             //最終ステージ番号
    #endregion

    void Start()
    {
        if (playerObj != null && continuePoint != null && continuePoint.Length > 0 && gameOverObj != null && fade != null && stageClearObj != null)
        {
            gameOverObj.SetActive(false);
            stageClearObj.SetActive(false);
            playerObj.transform.position = continuePoint[0].transform.position;
            p = playerObj.GetComponent<Player>();
            if (p == null)
            {
                Debug.Log("プレイヤー未設定");
            }
        }
        else
        {
            Debug.Log("設定が足りていません");
        }
    }

    void Update()
    {
        //ゲームオーバー時の処理
        if (GManager.instance.isGameOver && !doGameOver)
        {
            gameOverObj.SetActive(true);
            GManager.instance.PlaySE(gameOverSE);
            doGameOver = true;
        }
        //プレイヤーがやられた時の処理
        else if (p != null && p.IsContinueWaiting() && !doGameOver)
        {
            if (continuePoint.Length > GManager.instance.continueNum)
            {
                playerObj.transform.position = continuePoint[GManager.instance.continueNum].transform.position;
                p.ContinuePlayer();
            }
            else
            {
                Debug.Log("コンティニューポイント未設定");
            }
        }
        else if (stageClearTrigger != null && stageClearTrigger.isOn && !doGameOver && !doClear)
        {
            Debug.Log("Clear");
            Debug.Log(doClear);
            Debug.Log(GManager.instance.stageNum);

            StageClear();
            if (GManager.instance.stageNum >= finalStage)
            {
                doClear = true;
            }
            stageClearTrigger.isOn = false;
        }

        //ステージを切り替える
        if (fade != null && startFade && !doSceneChange && !doClear)
        {
            if (fade.IsFadeOutComplete())
            {
                //ゲームリトライ
                if (retryGame)
                {
                    GManager.instance.RetryGame();
                }
                //次のステージ
                else
                {
                    GManager.instance.stageNum = nextStageNum;
                }
                GManager.instance.isStageClear = false;
                SceneManager.LoadScene("stage" + nextStageNum);
                doSceneChange = true;
            }
        }
    }

    /// <summary>
    /// 最初から始める
    /// </summary>
    public void Retry()
    {
        GManager.instance.PlaySE(retrySE);
        ChangeScene(1);
        retryGame = true;
    }

    /// <summary>
    /// ステージを切り替えます。
    /// </summary>
    /// <param name="num">ステージ番号</param>
    public void ChangeScene(int num)
    {
        Debug.Log("Stage:" + num);

        if (fade != null)
        {
            Debug.Log("Stage:" + num);

            nextStageNum = num;
            fade.StartFadeOut();
            startFade = true;
        }
    }

    /// <summary>
    /// ステージクリア
    /// </summary>
    public void StageClear()
    {
        Debug.Log("StageClear");

        GManager.instance.continueNum = 0;
        GManager.instance.isStageClear = true;
        stageClearObj.SetActive(true);
        GManager.instance.PlaySE(stageClearSE);
    }

    /// <summary>
    /// タイトルへ戻る
    /// </summary>
    public void GotoTitle()
    {
        //ゲームクリアフラグOFF
        GManager.instance.isStageClear = false;
        //リトライ処理
        GManager.instance.RetryGame();
        //シーンロード
        SceneManager.LoadScene("TitleScene");
    }
}
