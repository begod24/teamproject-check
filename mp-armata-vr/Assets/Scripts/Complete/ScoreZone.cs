using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Complete
{
    public class ScoreZone : MonoBehaviour 
    {
        
        public TMP_Text scoreRemaining;
        public int remainingItems = 0;
        
        

        public void Awake()
        {
            
            remainingItems = GameObject.FindGameObjectsWithTag("Target").Length;
            
            UpdateText();
            
        }

        public void UpdateText()
        {
            scoreRemaining.text = remainingItems.ToString();
        }
    }
}   