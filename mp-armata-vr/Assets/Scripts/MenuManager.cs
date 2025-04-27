using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    public bool isMainMenuShowing = true;
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ToggleMenu()
    {
        if (isMainMenuShowing)
        {
            isMainMenuShowing = false;
            canvasGroup.alpha = 0;
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            return;
        }

        isMainMenuShowing = true;
        canvasGroup.alpha = 1;
        Time.timeScale = 0;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        isMainMenuShowing = false;
        canvasGroup.alpha = 0;
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
