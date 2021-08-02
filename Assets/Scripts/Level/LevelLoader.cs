using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button button;
    public string LevelName;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }


    private void onClick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);

        switch(levelStatus)
        {
            case LevelStatus.Locked:
                SoundManager.Instance.PlaySound(Sounds.ButtonLocked);
                break;
            case LevelStatus.Unlocked:
                SoundManager.Instance.PlaySound(Sounds.ButtonClick1);
                SceneManager.LoadScene(LevelName);
                break;
            case LevelStatus.Completed:
                SoundManager.Instance.PlaySound(Sounds.ButtonClick1);
                SceneManager.LoadScene(LevelName);
                break;
        }

    }
}
