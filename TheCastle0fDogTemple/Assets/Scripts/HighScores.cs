using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;	

[System.Serializable]
public struct Score
{
	public string name;
	public float value;
}

public class HighScores : MonoBehaviour {

	[Header("HighScore UI")]
	public GameObject m_highScoreUI;
	public Text[] m_scoreNames;
	public Text[] m_scoreValues;

	[Header("Max HighScores")]
	public uint m_maxHighScores;
	private uint m_recordedScores;
	private List<Score> m_scores = new List<Score>();

	private void Start()
	{
		m_recordedScores = (uint)PlayerPrefs.GetInt("recordedScores");
		for (int i = 0; i < m_recordedScores; ++i)
		{
			Score score;
			score.name = PlayerPrefs.GetString(string.Concat("ScoreName ", i.ToString()));
			score.value = PlayerPrefs.GetFloat(string.Concat("ScoreValue ", i.ToString()));
		}

		SortScores();
	}

	public void AddScore(Score score)
	{
		if (m_recordedScores < m_maxHighScores)
		{
			m_scores.Add(score);
			SortScores();
			m_recordedScores++;
		}
		else
		{
			if (score.value > m_scores[m_scores.Count - 1].value)
			{
				m_scores.Add(score);
				SortScores();
				m_scores.RemoveAt(m_scores.Count - 1);
			}
		}
	}

	public void ShowHighScores()
	{
		// Write to texts
		for (int i = 0; i < m_scores.Count; ++i)
		{
			m_scoreNames[i].text = m_scores[i].name;
			m_scoreValues[i].text = m_scores[i].value.ToString();
		}

		// Show texts
		m_highScoreUI.SetActive(true);
	}

	public void HideHighScores()
	{
		// Hide Texts
		m_highScoreUI.SetActive(false);
	}

	private void SortScores()
	{
		m_scores.Sort(delegate (Score lhs, Score rhs)
		{
			return lhs.value.CompareTo(rhs.value);
		});
	}

	public void WriteScores()
	{
		PlayerPrefs.SetInt("recordedScores", (int)m_recordedScores);

		for (int i = 0; i < m_scores.Count; ++i)
		{
			Score score = m_scores[i];

			PlayerPrefs.SetString(string.Concat("ScoreName ", i.ToString()), score.name);
			PlayerPrefs.SetFloat(string.Concat("ScoreValue ", i.ToString()), score.value);
		}

		PlayerPrefs.Save();
	}
}
