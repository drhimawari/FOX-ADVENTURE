using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DeadArea�ɓ������ꍇ�͔j������
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
