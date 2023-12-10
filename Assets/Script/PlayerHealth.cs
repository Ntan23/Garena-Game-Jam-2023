using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float currentHP;
    [SerializeField] private float maxHP;
    [SerializeField] private Image hpBar;
    private bool canBeDamaged = true;
    private bool isFirstTime = true;
    private GameManager gm;
    [SerializeField] private GameObject flashbang;
    [SerializeField] private GameObject gameOverUI;
    private AudioManager am;

    void Start() 
    {
        gm = GameManager.instance;
        am = AudioManager.instance;

        currentHP = maxHP;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        hpBar.fillAmount = currentHP/maxHP;
    }

    public void Heal(int health)
    {
        currentHP += health;

        if(currentHP > maxHP) currentHP = maxHP;

        UpdateHealthBar();
    }

    public void UpgradeMaxHP()
    {
        if(currentHP == maxHP) currentHP += 10;
           
        maxHP += 10;

        UpdateHealthBar();

        gm.CloseLevelUpUI();
    }

    private void UpdateFlashbangAlpha(float alpha) => flashbang.GetComponent<CanvasGroup>().alpha = alpha;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && canBeDamaged) 
        {
            if(currentHP > 0) am.PlayPlayerHitSFX();
            currentHP -= other.GetComponent<Enemy>().GetDamage();
            UpdateHealthBar();

            if(currentHP <= 0 & isFirstTime) 
            {
                am.StopAllSFX();
                am.PlayPlayerExplodeSFX();
                isFirstTime = false;
                Debug.Log("Game Over");
                LeanTween.value(flashbang, UpdateFlashbangAlpha, 0.0f, 1.0f, 1.0f).setOnComplete(() =>
                {
                    gameOverUI.SetActive(true);
                    LeanTween.value(flashbang, UpdateFlashbangAlpha, 1.0f, 0.0f, 2.0f).setOnComplete(() => {
                        Destroy(this.gameObject);
                    });
                });
            }

            StartCoroutine(DPS());
        }
    }

    IEnumerator DPS()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(0.5f);
        canBeDamaged = true;
    }

    public float GetHP()
    {
        return currentHP;
    }
}
