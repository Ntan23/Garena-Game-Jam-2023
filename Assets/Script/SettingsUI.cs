using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SFXSlider;
    private float bgmVolume;
    private float sfxVolume;
    private AudioManager am;
    
    private void Start()
    {
        am = AudioManager.instance;

        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.6f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1);

        StartCoroutine(ChangeVolume());
    }

    public void UpdateBGMSound(float value)
    {
        // MainMixer.SetFloat("BGM_Volume", value);
        am.SetBGMVolume(value);
        PlayerPrefs.SetFloat("BGMVolume", value);
    }

    public void UpdateSFXSound(float value)
    {
        //MainMixer.SetFloat("SFX_Volume", value);
        am.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void UpdateQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
    }

    private void UpdateAlpha(float alpha) => GetComponent<CanvasGroup>().alpha = alpha;

    public void OpenSettings()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        LeanTween.value(gameObject, UpdateAlpha, 0.0f, 1.0f, 0.5f);
    }

    public void CloseSettings() => LeanTween.value(gameObject, UpdateAlpha, 1.0f, 0.0f, 0.5f).setOnComplete(() =>
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    });

    IEnumerator ChangeVolume()
    {
        yield return new WaitForSeconds(0.01f);
        BGMSlider.value = bgmVolume;
        SFXSlider.value = sfxVolume;
        
        am.SetBGMVolume(bgmVolume);
        am.SetSFXVolume(sfxVolume);
    }
}
