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
    public AudioSource m_backgroundmusic;

	[Header("HighScore Stuff")]
	private HighScores m_highScores;
	private bool m_hasName = false;
	public GameObject m_InputGroup;
	public InputField m_input;




	// Use this for initialization
	void Start ()
    {
        Timer = 0;
		m_highScores = GetComponent<HighScores>();


		m_backgroundmusic.Play();
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


        // Pressing Escape Opens up a pause menu
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
        // setting lose to true 
        if (m_scoreCount <= 0)
        {
            lose = true;
        }
        // checking to see if you win
        if (win == true)
        {
            m_Hud.SetActive(false);
            winScreen.SetActive(true);
            Time.timeScale = 0;

			// Highscore Win Stuff

			// Input Name for score
			if (!m_hasName)
			{
				m_InputGroup.SetActive(true);
			}
			else
			{
				m_highScores.ShowHighScores();
			}

		}
        // checking to see if you lose 
        if (lose == true)
        {
            m_Hud.SetActive(false);
            loseScreen.SetActive(true);
            Time.timeScale = 0;

			// Lose Stuff
			m_highScores.ShowHighScores();
        }
	}

    // resuming the game 
    public void Resume()
    {
        m_PauseMenu.SetActive(false);
        m_Hud.SetActive(true);
        Time.timeScale = 1;
    }

    // adding score to your total score
    public void AddScore(int score)
    {
        m_scoreCount += score;
    }

    // taking score off your total score 
    public void DmgScore(int score)
    {
        m_scoreCount -= score;
    }

    // a public funtaion to be told if win is true or not
    public void doesWin(bool yes)
    {
        if (yes == true)
        {
            win = true;
        }
    }

	public void AddHighScore()
	{
		m_InputGroup.SetActive(false);

		Score score;
		score.name = m_input.text;
		score.value = m_scoreCount;

		m_highScores.AddScore(score);
		m_highScores.WriteScores();

		m_hasName = true;
	}
}
