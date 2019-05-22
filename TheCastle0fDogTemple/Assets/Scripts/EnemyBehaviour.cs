using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int m_health;
    public int m_addScore;

    private Hud hud;
    

	// Use this for initialization
	void Start () 
    {
        hud = FindObjectOfType<Hud>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_health <= 0)
        {
            hud.AddScore(m_addScore);
            Destroy(gameObject);
        }
	}
    
    public void TakeDamage(int damage)
    {
        m_health -= damage;
    }
}
