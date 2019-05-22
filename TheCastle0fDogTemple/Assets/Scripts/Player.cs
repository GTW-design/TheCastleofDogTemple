using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{

	private CharacterController charControl;
	private float movement;
	private float rotation;
	private float fwd = 0;
	private float turn = 0;

	[Header("Inputs")]
	public KeyCode forwards;
	public KeyCode backwards;
	public KeyCode left;
	public KeyCode right;

	public KeyCode attack;
	public int attackButton;

	[Header("Multipliers")]
	public float moveMulti;
	public float turnMulti;

	[Header("Maximums")]
	public float maxSpeed;
	public float maxRot;

	[Header("Attack")]
	public float attackRange;
	public string enemyTag;
	public int attackDamage;


	// Use this for initialization
	void Start()
	{
		charControl = gameObject.GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		// Rotate Input
		if (Input.GetKey(left))
		{
			turn -= Time.deltaTime;
		}
		else if (Input.GetKey(right))
		{
			turn += Time.deltaTime;
		}
		else
		{
			turn *= 0.80f;
			if (Mathf.Abs(turn) < 0.05f)
				turn = 0;
		}
		turn = Mathf.Clamp(turn, -1, 1);


		// Movement Input
		if (Input.GetKey(forwards))
		{
			fwd += Time.deltaTime;
		}
		else if (Input.GetKey(backwards))
		{
			fwd -= Time.deltaTime;
		}
		else
		{
			fwd *= 0.90f;
			if (Mathf.Abs(fwd) < 0.05f)
				fwd = 0;
		}
		fwd = Mathf.Clamp(fwd, -1, 1);

		// ROTATE

		// Reverse rotation if going backwards
		int invert = 1;
		if (movement < 0)
			invert = -1;


		rotation = Mathf.LerpUnclamped(0, maxRot, turn);
		rotation = Mathf.Clamp(rotation, -maxRot, maxRot);

		rotation *= invert;

		transform.Rotate(Vector3.up * rotation * turnMulti);

		// MOVE
		movement = Mathf.LerpUnclamped(0, maxSpeed, fwd);
		movement = Mathf.Clamp(movement, -maxSpeed, maxSpeed);

		charControl.SimpleMove(transform.forward * movement * moveMulti);


		// ATTACK STUFF
		if (Input.GetKeyDown(attack) || Input.GetMouseButtonDown(attackButton))
		{
			RaycastHit rayHit;

			if (Physics.Raycast(transform.position, transform.forward, out rayHit, 5.0f))
			{
				// DO STUFF
				var hit = rayHit.collider.gameObject;
				if (hit.CompareTag(enemyTag))
				{
					hit.GetComponent<EnemyBehaviour>().TakeDamage(attackDamage);
				}
			}

		}
	}
}
