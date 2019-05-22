using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
	public TriggerTarget m_target;

	public bool m_isSingleUse = false;

	// Use this for initialization
	void Start ()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!m_target)
			return;

		if (other.tag != "Player")
			return;
	}

	private void OnTriggerExit(Collider other)
	{
		if (m_isSingleUse)
			return;

		if (!m_target)
			return;

		if (other.tag != "Player")
			return;
	}
}
