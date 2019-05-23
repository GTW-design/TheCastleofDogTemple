using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSucker : MonoBehaviour
{
    public GameObject m_player;
    public Transform m_spawnPoint;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_player)
        {
            m_player.transform.position = m_spawnPoint.position;
        }
    }
}
