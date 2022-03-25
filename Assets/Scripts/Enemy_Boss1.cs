using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss1 : MonoBehaviour
{
    #region//�C���X�y�N�^�[�Őݒ肷��
    [Header("���Z�X�R�A")] public int myScore;
    [Header("�ړ����x")] public float speed;
    [Header("�d��")] public float gravity;
    [Header("��ʊO�ł��s������")] public bool nonVisibleAct;
    [Header("�ڐG����")] public EnemyCollisionCheck checkCollision;
    [Header("���j���ꂽ����SE")] public AudioClip deadSE;
    [Header("�v���C���[�ڐG����")] public EnemyCollisionPlayerCheck checkPlayerCollision1;
    [Header("�v���C���[�ڐG����")] public EnemyCollisionPlayerCheck checkPlayerCollision2;
    [Header("�v���C���[")] public GameObject p;
    [Header("�{�X�̗�")] public int hp;
    [Header("�{�X���jSE")] public AudioClip bossbreakSE;
    [Header("�N���ABGM")] public AudioClip stageClearBGM;
    [Header("���I�u�W�F�N�g")] public GameObject bridge;
    #endregion

    #region//�v���C�x�[�g�ϐ�
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
        //�_���[�W�����o
        if (isDamage)
        {
            //����-���Ă��鎞�ɖ߂�
            if (blinkTime > 0.2f)
            {
                sr.enabled = true;
                blinkTime = 0.0f;
            }
            //����-�����Ă���Ƃ�
            else if (blinkTime > 0.1f)
            {
                sr.enabled = false;
            }
            //����-���Ă���Ƃ�
            else
            {
                sr.enabled = true;
            }
            //1�b�������疾�ŏI���
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
            //�ʏ펞
            if (sr.isVisible || nonVisibleAct)
            {
                if (checkPlayerCollision1.isOn || checkPlayerCollision2.isOn)
                {
                    //�i�s�����ݒ�
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
                    //�ړ�
                    rb.velocity = new Vector2(xVector * speed, -gravity);
                    anim.SetBool("run", true);
                }
                else
                {
                    //�~�܂�
                    rb.velocity = new Vector2(0f * speed, -gravity);
                    anim.SetBool("run", false);
                }
            }
            else
            {
                //Rigidbody2D��~
                rb.Sleep();
            }
        }
        else
        {
            //�_���[�W��
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
                        //�X�R�A���Z
                        GManager.instance.score += myScore;
                    }
                    Destroy(gameObject, 3f);

                    //�{�X���j���o
                    //BGM��~
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
                //��]���o
                transform.Rotate(new Vector3(0, 0, 5));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //��e������
        if (collision.collider.tag == "PlayerAttack")
        {
            if (isDamage) return;

            //�̗͂����炷
            --hp;

            //�t���O�ݒ�
            isDamage = true;

            //�m�b�N�o�b�N����
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
