using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
	public int m_health;
    public int m_addScore;
    public AudioSource[] m_enemyhurt;

	[Header("Nav")]
	public Transform[] m_nodes = new Transform[2];
	private uint m_targetNodeIndex = 0;
	private NavMeshAgent m_navAgent;

    private Hud m_hud;

	private Animator m_animator;

    private bool dead = false;
    

	// Use this for initialization
	void Start () 
    {
        m_hud = FindObjectOfType<Hud>();
        m_navAgent = GetComponent<NavMeshAgent>();

        m_navAgent.SetDestination(m_nodes[m_targetNodeIndex].position);

		m_animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (dead)
            return;

        if (m_health <= 0)
        {
            dead = true;
            m_hud.AddScore(m_addScore);
			m_animator.ResetTrigger("isWalking");
			m_animator.SetTrigger("isEnemyDead");
            Destroy(gameObject, 2.5f);
            Collider collider = GetComponent<Collider>();
            collider.enabled = false;

            m_navAgent.isStopped = true;
            return;
        }

		// NAV 
		Vector3 currentPos = transform.position;
		currentPos.y = 0;
		Vector3 targetPos = m_nodes[m_targetNodeIndex].position;
		targetPos.y = 0;
		float distance = Vector3.SqrMagnitude(currentPos - targetPos);

        m_animator.SetTrigger("isWalking");

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
        m_enemyhurt[Random.Range(0, m_enemyhurt.Length)].Play();
		m_animator.ResetTrigger("isWalking");
		m_animator.SetTrigger("isFlinchForward");
	}
}
