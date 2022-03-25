using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss1 : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("加算スコア")] public int myScore;
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動する")] public bool nonVisibleAct;
    [Header("接触判定")] public EnemyCollisionCheck checkCollision;
    [Header("撃破された時のSE")] public AudioClip deadSE;
    [Header("プレイヤー接触判定")] public EnemyCollisionPlayerCheck checkPlayerCollision1;
    [Header("プレイヤー接触判定")] public EnemyCollisionPlayerCheck checkPlayerCollision2;
    [Header("プレイヤー")] public GameObject p;
    [Header("ボス体力")] public int hp;
    [Header("ボス撃破SE")] public AudioClip bossbreakSE;
    [Header("クリアBGM")] public AudioClip stageClearBGM;
    [Header("橋オブジェクト")] public GameObject bridge;
    #endregion

    #region//プライベート変数
    private Rigidbody2D rb = null;
    private SpriteRenderer sr = null;
    private Animator anim = null;
    private ObjectCollision oc = null;
    private BoxCollider2D col = null;
    private bool rightTleftF = false;
    private bool isDead = false;

    private float blinkTime = 0.0f;
    private float continueTime = 0.0f;
    private bool isDamage = false;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //ダメージ時演出
        if (isDamage)
        {
            //明滅-ついている時に戻る
            if (blinkTime > 0.2f)
            {
                sr.enabled = true;
                blinkTime = 0.0f;
            }
            //明滅-消えているとき
            else if (blinkTime > 0.1f)
            {
                sr.enabled = false;
            }
            //明滅-ついているとき
            else
            {
                sr.enabled = true;
            }
            //1秒たったら明滅終わり
            if (continueTime > 1.0f)
            {
                isDamage = false;
                blinkTime = 0.0f;
                continueTime = 0.0f;
                sr.enabled = true;
            }
            else
            {
                blinkTime += Time.deltaTime;
                continueTime += Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDamage)
        {
            //通常時
            if (sr.isVisible || nonVisibleAct)
            {
                if (checkPlayerCollision1.isOn || checkPlayerCollision2.isOn)
                {
                    //進行方向設定
                    int xVector = -1;
                    if (transform.position.x < p.transform.position.x)
                    {
                        rightTleftF = !rightTleftF;
                        xVector = 1;
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        rightTleftF = rightTleftF;
                        xVector = -1;
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    //移動
                    rb.velocity = new Vector2(xVector * speed, -gravity);
                    anim.SetBool("run", true);
                }
                else
                {
                    //止まる
                    rb.velocity = new Vector2(0f * speed, -gravity);
                    anim.SetBool("run", false);
                }
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
                if (hp<=0)
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

                    //ボス撃破演出
                    //BGM停止
                    GManager.instance.PlaySE(bossbreakSE);
                    GameObject BGM = GameObject.Find("BGM");
                    AudioSource GMAudio = BGM.GetComponent<AudioSource>();
                    GMAudio.Stop();
                    GMAudio.clip = stageClearBGM;
                    GMAudio.Play();
                    bridge.SetActive(true);
                }
            }
            else
            {
                //回転演出
                transform.Rotate(new Vector3(0, 0, 5));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //被弾時処理
        if (collision.collider.tag == "PlayerAttack")
        {
            if (isDamage) return;

            //体力を減らす
            --hp;

            //フラグ設定
            isDamage = true;

            //ノックバック処理
            if (transform.position.x < p.transform.position.x)
            {
                transform.position = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z);
            }
        }
    }
}
