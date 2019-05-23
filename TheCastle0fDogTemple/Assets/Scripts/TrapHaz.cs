using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapHaz : MonoBehaviour
{
    public float m_ScoreTimer;
    public GameObject m_player;
    public int m_dmgScore;

    private Hud hud;
    private Player player;
    private float theTime;

    private bool inside;

	// Use this for initialization
	void Start ()
    {
        theTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        theTime -= Time.deltaTime;

        if (inside == true)
        {
            player.m_slowdown = true;
            if(theTime <= 0)
            {
                hud.AddScore(-m_dmgScore);
                theTime = m_ScoreTimer;
            }
        }
        else
        {
            if (theTime <= 0)
            {
                player.m_slowdown = false;

            }
            
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_player)
        {
            inside = true;
            theTime = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_player)
        {
            inside = false;
            theTime = m_ScoreTimer;
        }

    }
}
