using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Zako2 : MonoBehaviour
{
    public GameObject blockPrefab;

    #region//インスペクターで設定する
    [Header("加算スコア")] public int myScore;
    [Header("速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも動作する")] public bool nonVisibleAct;
    [Header("衝突チェック")] public EnemyCollisionCheck checkCollision;
    [Header("撃破された時のSE")] public AudioClip deadSE;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();

        //コルーチン開始
        InvokeRepeating("Spawn", 1f, 1f);
    }

    private void Spawn()
    {
        //落とす岩の生成
        Instantiate(blockPrefab, transform.position, transform.rotation); 
    }

    void FixedUpdate()
    {
        if (!oc.playerStepOn)
        {
            if (sr.isVisible || nonVisibleAct)
            {
                //進行方向の設定
                if (checkCollision.isOn)
                {
                    rightTleftF = !rightTleftF;
                }

                //移動
                int xVector = -1;
                if (rightTleftF)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                rb.velocity = new Vector2(xVector * speed, -gravity);
            }
            else
            {
                //Rigidbody2D停止
                rb.Sleep();
            }
        }
        else
        {
            //ダメージ時
            if (!isDead)
            {
                anim.Play("dead");
                rb.velocity = new Vector2(0, -gravity);
                isDead = true;
                col.enabled = false;
                if (GManager.instance != null)
                {
                    //SE
                    GManager.instance.PlaySE(deadSE);
                    //スコア加算
                    GManager.instance.score += myScore;
                }
                Destroy(gameObject, 3f);
            }
            else
            {
                //回転演出
                transform.Rotate(new Vector3(0, 0, 5));
            }
        }
    }
}
