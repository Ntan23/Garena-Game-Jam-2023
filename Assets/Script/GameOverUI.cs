using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    private int clickCount;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private Fade fade;
    [SerializeField] private TextMeshProUGUI scoreAndHighscoreText;
    private GameManager gm;

    public void UpdateScoreTextAlpha(float alpha) => scoreText.GetComponent<CanvasGroup>().alpha = alpha;
    
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueButton()
    {
        clickCount++;

        if(clickCount == 1)
        {
            gameOverText.SetActive(false);
            
            LeanTween.value(scoreText, UpdateScoreTextAlpha, 0.0f, 1.0f, 0.5f);

            scoreAndHighscoreText.text = "Score : " + gm.GetScore().ToString() + "\nHighscore : " + PlayerPrefs.GetInt("Highscore").ToString();
        }
        else 
        {
            fade.BackToMainMenu();
        }
    }
}
