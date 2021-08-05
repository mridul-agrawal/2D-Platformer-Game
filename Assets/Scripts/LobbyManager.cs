using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public GameObject LevelSelector;

    public void PlayButton()
    {
        SoundManager.Instance.PlaySound(Sounds.ButtonClick2);
        LevelSelector.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
