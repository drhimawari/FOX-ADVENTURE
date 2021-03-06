using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionPlayerCheck : MonoBehaviour
{
    /// <summary>
    /// ???????ɓG?܂??͕ǂ?????
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private string groundTag = "Ground";
    private string enemyTag = "Enemy";
    private string playerTag = "Player";

    #region//?ڐG????
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == playerTag)
        {
            isOn = false;
        }
    }
    #endregion
}
