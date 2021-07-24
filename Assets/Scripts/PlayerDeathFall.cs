using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathFall : MonoBehaviour
{
    public Camera mainCamera;
    public CanvasRenderer deathUIPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            mainCamera.transform.parent = null;
            deathUIPanel.gameObject.SetActive(true);
            other.GetComponent<PlayerController>().gameObject.SetActive(false);
        }
    }
}
