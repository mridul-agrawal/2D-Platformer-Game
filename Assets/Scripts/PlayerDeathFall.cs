using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathFall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().DecreaseHealth();
        }
    }
}
