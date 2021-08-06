using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject soundManager;

    private void Awake()
    {
        if(SoundManager.Instance == null)
        {
            Instantiate(soundManager);
        }
    }
}
