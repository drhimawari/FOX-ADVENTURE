using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DeadAreaに入った場合は破棄する
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
