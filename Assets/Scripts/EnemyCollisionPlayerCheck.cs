using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionPlayerCheck : MonoBehaviour
{
    /// <summary>
    /// ”»’è“à‚É“G‚Ü‚½‚Í•Ç‚ª‚ ‚é
    /// </summary>
    [HideInInspector] public bool isOn = false;

    private string groundTag = "Ground";
    private string enemyTag = "Enemy";
    private string playerTag = "Player";

    #region//ÚG”»’è
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
