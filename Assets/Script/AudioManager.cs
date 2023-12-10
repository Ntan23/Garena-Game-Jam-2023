using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager instance {get; private set;}
    void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion
    [SerializeField] private Sound[] sounds;

    private void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);

        if(s == null) return;

        s.source.Play();
    }

    private void Stop(string name)
    { 
        Sound s = System.Array.Find(sounds, sound => sound.name == name);

        if(s == null) return;

        s.source.Stop();
    }

    void Start() 
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixer;
        }
    }

    public void SetSFXVolume(float volume)
    {
        foreach(Sound s in sounds) 
        {
            if(s.name == "BGM1" && s.name == "BGM2") continue;
            if(s.name != "BGM1" && s.name != "BGM2") s.source.volume = volume;
        }
    }

    public void SetBGMVolume(float volume)
    {
        foreach(Sound s in sounds) 
        {
            if(s.name == "BGM1") s.source.volume = volume;
               
            if(s.name == "BGM2") s.source.volume = volume;  
        }
    }

    public void StopAllSFX()
    {
        foreach(Sound s in sounds) 
        {
            if(s.name != "BGM") s.source.Stop();
        }
    }

    public void PlayDialogueActionSFX(string name) => Play(name);
    
    public void PlayBGM1() => Play("BGM1");
    public void StopBGM1() => Stop("BGM1");
    public void PlayBGM2() => Play("BGM2");
    public void StopBGM2() => Stop("BGM2");
    public void PlayBossIncomingSFX() => Play("BossIncoming");
    public void PlayEnemyHitSFX() => Play("EnemyHit");
    public void PlayEnemyExplodeSFX() => Play("EnemyExplode");
    public void PlayHoverSFX() => Play("HoverButton");
    public void PlayPlayerHitSFX() => Play("PlayerHit");
    public void PlayPlayerExplodeSFX() => Play("PlayerExplode");
    public void PlayPlayerShootSFX() => Play("PlayerShoot");
    public void PlayButtonPressedSFX() => Play("ButtonPressed");
    public void PlayUpgradeSFX() => Play("UpgradeSound");
    public void PlayEndShootSFX() => Play("EndShoot");

} 

