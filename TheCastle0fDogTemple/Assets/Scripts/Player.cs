using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

	private CharacterController m_charControl;
	private float m_movement;
	private float m_rotation;
	private float m_fwd = 0;
	private float m_turn = 0;

	[Header("Inputs")]
	public KeyCode m_forwards = KeyCode.W;
	public KeyCode m_backwards = KeyCode.S;
	public KeyCode m_left = KeyCode.A;
	public KeyCode m_right = KeyCode.D;

	public KeyCode m_attack = KeyCode.Return;
	public int m_attackButton = 0;

	public bool m_invert = true;

	[Header("Multipliers")]

	[Range(1, 5)]
	public float m_moveMulti;
	[Range(1, 5)]
	public float m_turnMulti;
	[Range(1, 5)]
	public float m_slowMulti;

    public bool m_slowdown = false;

	[Header("Maximums")]
	public float m_maxSpeed;
	public float m_maxRot;

	[Header("Attack")]
	public string m_enemyTag;
	public Transform m_attackOrigin;
	public float m_attackRange;
	public int m_attackDamage;



	// Use this for initialization
	void Start()
	{
		m_charControl = gameObject.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		// Rotate Input
		if (Input.GetKey(m_left))
		{
			m_turn -= Time.deltaTime;
		}
		else if (Input.GetKey(m_right))
		{
			m_turn += Time.deltaTime;
		}
		else
		{
			m_turn *= 0.80f;
			if (Mathf.Abs(m_turn) < 0.05f)
				m_turn = 0;
		}
		m_turn = Mathf.Clamp(m_turn, -1, 1);


		// Movement Input
		if (Input.GetKey(m_forwards))
		{
			m_fwd += Time.deltaTime;
		}
		else if (Input.GetKey(m_backwards))
		{
			m_fwd -= Time.deltaTime;
		}
		else
		{
			m_fwd *= 0.90f;
			if (Mathf.Abs(m_fwd) < 0.05f)
				m_fwd = 0;
		}
		m_fwd = Mathf.Clamp(m_fwd, -1, 1);

		// ROTATE

		// Reverse rotation if going backwards
		int invert = 1;
		if (m_movement < 0)
			invert = -1;


		m_rotation = Mathf.LerpUnclamped(0, m_maxRot, m_turn);
		m_rotation = Mathf.Clamp(m_rotation, -m_maxRot, m_maxRot);

		if (m_invert)
			m_rotation *= invert;

		transform.Rotate(Vector3.up * m_rotation * m_turnMulti);

		// Check if slowed
		float slowDown = 1.0f;
		if (m_slowdown)
			slowDown = m_slowMulti;

		// MOVE
		m_movement = Mathf.LerpUnclamped(0, m_maxSpeed, m_fwd);
		m_movement = Mathf.Clamp(m_movement, -m_maxSpeed, m_maxSpeed);

		m_charControl.SimpleMove(transform.forward * m_movement * m_moveMulti / slowDown);


		// ATTACK STUFF
		if (Input.GetKeyDown(m_attack) || Input.GetMouseButtonDown(m_attackButton))
		{
			RaycastHit rayHit;

			if (Physics.Raycast(m_attackOrigin.position, transform.forward, out rayHit, 5.0f))
			{
				// DO STUFF
				var hit = rayHit.collider.gameObject;
				if (hit.CompareTag(m_enemyTag))
				{
					hit.GetComponent<EnemyBehaviour>().TakeDamage(m_attackDamage);
				}
			}
		}
	}
}
