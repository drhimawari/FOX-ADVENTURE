using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DeadArea‚É“ü‚Á‚½ê‡‚Í”jŠü‚·‚é
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
