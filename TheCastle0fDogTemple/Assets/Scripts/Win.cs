using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    private Hud hud;
    private GameObject player;

    private void Start()
    {
        hud = FindObjectOfType<Hud>();
        player = FindObjectOfType<Player>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            hud.doesWin(true);
        }
    }


}
