using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        public GameObject player;
        private void Awake()
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            player.GetComponent<BallControl>().enabled = false;
            StartCoroutine(dialogueSequence());
        }

        private IEnumerator dialogueSequence()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            gameObject.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<BallControl>().enabled = true;
        }

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }

    }
    
}


