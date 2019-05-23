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

    private bool inside = false;
	private bool inUse = false;

	// Use this for initialization
	void Start ()
    {
        theTime = 0;
		player = m_player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (!inUse)
			return;

        theTime -= Time.deltaTime;

        if (inside == true)
        {
            player.m_slowdown = true;
            if(theTime <= 0)
            {
                hud.DmgScore(m_dmgScore);
                theTime = m_ScoreTimer;
            }
        }
        else
        {
            if (theTime <= 0)
            {
                player.m_slowdown = false;
				inUse = false;
            }
            
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_player)
        {
            inside = true;
            theTime = 0;
			inUse = true;

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
