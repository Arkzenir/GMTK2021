using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class doorTrigger : MonoBehaviour
{
    public GameObject doorKey;
    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            doorKey.SetActive(true);



        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            doorKey.SetActive(false);
        }
    }
}
