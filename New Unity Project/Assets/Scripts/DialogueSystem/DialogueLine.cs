using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        
        private Text textHolder;
        [SerializeField]
        [TextArea]
        private string input;
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private void Awake()
        {
            textHolder = GetComponent<Text>();
            imageHolder.sprite = characterSprite;

        }

        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder));
        }
    }
    
}


