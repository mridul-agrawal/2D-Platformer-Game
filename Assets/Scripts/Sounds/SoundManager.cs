using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    
    public AudioSource audioEffects;
    public AudioSource audioMusic;
    public SoundType[] AudioList;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(Sounds soundType)
    {
        AudioClip clip = GetSoundClip(soundType);
        if(clip!=null)
        {
            audioEffects.PlayOneShot(clip);
        } else
        {
            Debug.LogError("No Audio Clip got selected");
        }
    }

    public AudioClip GetSoundClip(Sounds soundType)
    {
        SoundType st = Array.Find(AudioList, item => item.soundType == soundType);
        if(st != null)
        {
            return st.audio;
        }
        return null;
    }

    public void StopSoundEffect()             // Sets the audio clip to null.
    {
        audioEffects.clip = null;
    }

}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip audio;
}

public enum Sounds
{
    BackgroundMusic,
    ButtonClick1,
    ButtonClick2,
    ButtonLocked,
    PlayerMove,
    PlayerJump,
    PlayerLand,
    PlayerDeath,
    EnemyDeath,
    GameOver,
    NewLevel,
    LevelComplete
}
