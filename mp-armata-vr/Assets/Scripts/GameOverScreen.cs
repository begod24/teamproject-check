using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public GameObject background; // назначь в инспекторе (например, панель)

    public void Setup()
    {
        gameObject.SetActive(true);           // включаем Canvas/GameObject
        if (background != null)
            background.SetActive(true);       // включаем фон, если он отдельно
        Time.timeScale = 0f;                  // останавливаем игру
    }
}
