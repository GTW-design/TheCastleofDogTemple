﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    private float Timer;
    private bool win = false;
    private bool lose = false;

    public int m_scoreCount;
    public Text m_score;
    public GameObject m_PauseMenu;
    public GameObject m_Hud;
    public int m_negate;
    public GameObject winScreen;
    public GameObject loseScreen;




	// Use this for initialization
	void Start ()
    {
        Timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;
        if (Timer > 1)
        {
            Timer = 0;
            m_scoreCount -= m_negate;
        }
        m_score.text = m_scoreCount.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && win == false && lose == false)
        {
            if (m_PauseMenu.activeSelf != true)
            {
                m_PauseMenu.SetActive(true);
                m_Hud.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                m_PauseMenu.SetActive(false);
                m_Hud.SetActive(true);
                Time.timeScale = 1;
            }
        }

        if (win == true)
        {
            m_Hud.SetActive(false);
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
        if (lose == true)
        {
            m_Hud.SetActive(false);
            loseScreen.SetActive(true);
            Time.timeScale = 0;
        }
	}


    void Resume()
    {
        m_PauseMenu.SetActive(false);
        m_Hud.SetActive(true);
        Time.timeScale = 1;
    }

    public void AddScore(int score)
    {
        m_scoreCount += score;
    }

    public void DmgScore(int score)
    {
        m_scoreCount -= score;
    }

    public void doesWin(bool yes)
    {
        if (yes == true)
        {
            win = true;
        }
    }
}
