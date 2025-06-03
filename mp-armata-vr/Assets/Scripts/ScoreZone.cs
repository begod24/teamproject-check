using System;
using TMPro;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
   public TMP_Text scoreRemainingText;
   public TMP_Text MaxItemsText;
   public TMP_Text PointText;
   public TMP_Text GameOverPointsText;
   private int _remainingItems = 0;
   private int _maxItems = 9;
   private int _points = 0;
   public GameOverScreen gameOverScreen;

   
   private void Awake()
   {
      _remainingItems = GameObject.FindGameObjectsWithTag("Target").Length;
      UpdateText();
   }

   

   private void UpdateText()
   {
      scoreRemainingText.text = _remainingItems.ToString();
      MaxItemsText.text = _maxItems.ToString();
      PointText.text = _points.ToString();
      GameOverPointsText.text = _points.ToString();
      
   }

   private void OnTriggerEnter(Collider other)
   {
      if (!other.CompareTag("Target")) return;

      _remainingItems--;
      _points++;
      UpdateText();
      Destroy(other.gameObject);
      if (_remainingItems <= 0)
      {
         if (gameOverScreen != null)
            gameOverScreen.Setup();
      }

      
   }
}
