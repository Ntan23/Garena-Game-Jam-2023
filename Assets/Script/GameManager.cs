using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        if(instance == null) instance = this;
    }
    #endregion

    private float enemiesCount;
    private float currentXP;
    [SerializeField] private float maxXP;
    private int highscore;
    #region ForEnemy
    private float enemyChaserSpeed = 3.0f;
    private float enemyTankerHealth = 80.0f;
    private float bossHealth = 200.0f;
    private float bossDamage = 20.0f;
    private float bossSpeed = 1.5f;
    #endregion
    private int currentLevel = 1;
    private int currentScore;
    private bool isPause;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private Transform enemyParent;
    [SerializeField] private Image XPBar;
    [SerializeField] private GameObject levelUpUI;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private CameraShake camShake;
    private AudioManager am;

    void Start() 
    {
        am = AudioManager.instance;

        enemiesCount = waveSpawner.waves.enemiesCount;

        highscore = PlayerPrefs.GetInt("Highscore", 0);

        highscoreText.text = "Highscore : " + highscore.ToString();

        UpdateXPBar();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPause) 
        {
            isPause = true;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            isPause = false;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void CheckEnemy()
    {
        Debug.Log("Checked");

        if(enemyParent.childCount <= 1)
        {
            waveSpawner.UpgradeWave();
            enemiesCount = waveSpawner.waves.enemiesCount;
        }
    }

    public void NextWave()
    {
        waveSpawner.UpgradeWave();
        enemiesCount = waveSpawner.waves.enemiesCount;
    }

    public void AddXP(int XP)
    {
        currentXP += XP;

        UpdateXPBar();

        if(currentXP >= maxXP) LevelUp();
    }

    private void UpdateXPBar() 
    {
        XPBar.fillAmount = currentXP/maxXP;
    }

    public void LevelUp()
    {
        currentLevel++;
        am.PlayUpgradeSFX();

        levelText.text = "Lv. " + currentLevel.ToString();

        currentXP = 0;
        maxXP = Mathf.Floor(maxXP + 0.15f * maxXP);

        UpdateXPBar();

        levelUpUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void CloseLevelUpUI() 
    {
        levelUpUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void UpgradeEnemyStats()
    {
        //Chaser
        enemyChaserSpeed = enemyChaserSpeed + (0.1f * enemyChaserSpeed);

        //Tanker
        enemyTankerHealth = enemyTankerHealth + (0.1f * enemyTankerHealth);

        //Boss
        bossHealth = bossHealth + (0.1f * bossHealth);
        bossDamage = bossDamage + (0.1f * bossDamage);
        bossSpeed = bossSpeed + (0.1f * bossSpeed);
    }

    public void AddScore(int score)
    {
        currentScore += score;

        scoreText.text = "Score : " + currentScore.ToString();

        if(currentScore > highscore) 
        {
            highscoreText.text = "Highscore : " + currentScore.ToString();

            PlayerPrefs.SetInt("Highscore", currentScore);
        }
    }

    public void CameraShakeEffect() => camShake.ShakeCamera(5.0f, 2.0f);

    public float GetEnemyChaserSpeed() 
    {
        return enemyChaserSpeed;
    }

    public float GetEnemyTankerHealth()
    {
        return enemyTankerHealth;
    }

    public float GetBossHealth()
    {
        return bossHealth;
    }

    public float GetBossDamage()
    {
        return bossDamage;
    }

    public float GetBossSpeed()
    {
        return bossSpeed;
    }

    public int GetScore()
    {
        return currentScore;
    }
}
