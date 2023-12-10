using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private AudioManager am;

    void Start() 
    {
        am = AudioManager.instance;

        FadeIn();
    }

    private void UpdateAlpha(float alpha) => GetComponent<CanvasGroup>().alpha = alpha;

    private void FadeIn() 
    {
        LeanTween.value(gameObject, UpdateAlpha, 1.0f, 0.0f, 0.5f).setOnComplete(() => {
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            if(SceneManager.GetActiveScene().buildIndex == 0) 
            {
                am.PlayBGM1();
                am.StopBGM2();
            }

            if(SceneManager.GetActiveScene().buildIndex == 1) 
            {
                am.PlayBGM2();
                am.StopBGM1();
            }
        });
    }

    public void BackToMainMenu()
    {
        LeanTween.value(gameObject, UpdateAlpha, 0.0f, 1.0f, 0.5f).setOnComplete(() => {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            SceneManager.LoadSceneAsync("Main Menu");
        });
    }

    public void GoToGameScene()
    {
        LeanTween.value(gameObject, UpdateAlpha, 0.0f, 1.0f, 0.5f).setOnComplete(() => {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            SceneManager.LoadSceneAsync("GameScene");
        });
    }

    public void QuitGame()
    {
        LeanTween.value(gameObject, UpdateAlpha, 0.0f, 1.0f, 0.5f).setOnComplete(() => {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            Application.Quit();
        });
    }
}
