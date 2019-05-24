using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
	public int m_health;
    public int m_addScore;

	[Header("Nav")]
	public Transform[] m_nodes = new Transform[2];
	private uint m_targetNodeIndex = 0;
	private NavMeshAgent m_navAgent;

    private Hud m_hud;
    

	// Use this for initialization
	void Start () 
    {
        m_hud = FindObjectOfType<Hud>();
        m_navAgent = GetComponent<NavMeshAgent>();

        m_navAgent.SetDestination(m_nodes[m_targetNodeIndex].position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_health <= 0)
        {
            m_hud.AddScore(m_addScore);
            Destroy(gameObject);
        }

		// NAV 
		Vector3 currentPos = transform.position;
		currentPos.y = 0;
		Vector3 targetPos = m_nodes[m_targetNodeIndex].position;
		targetPos.y = 0;
		float distance = Vector3.SqrMagnitude(currentPos - targetPos);

        // IF at target node
        if (distance <= 1.0f)
        {
            m_targetNodeIndex = 1 - m_targetNodeIndex;
            m_navAgent.SetDestination(m_nodes[m_targetNodeIndex].position);
        }
       
	}
    
    public void TakeDamage(int damage)
    {
        m_health -= damage;
    }
}
