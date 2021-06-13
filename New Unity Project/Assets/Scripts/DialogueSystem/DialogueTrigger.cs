using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        public GameObject dialogueBox;

        private void OnTriggerEnter2D(Collider2D player)
        {
            if (player.gameObject.tag == "Player")
            {
                dialogueBox.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
    
}


